using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using XMLtoPDF.Application.Interfaces;
using SE.GMO.Billing.Application.Options;
using XMLtoPDF.Infrastructure.Interfaces;
using XMLtoPDF.Domain.Entities;

namespace XMLToPDF.FileProcessor.Jobs
{
    public class XmlProcessorJob : IJob
    {
        private readonly IXmlFileLoader _fileLoader;
        private readonly IXmlValidator _validator;
        private readonly ILogger<XmlProcessorJob> _logger;

        private readonly IProcessedFileRepository _repository;

        private readonly string _inputFolder;
        private readonly string _processingFolder;
        private readonly string _processedFolder;
        private readonly string _errorFolder;

        public XmlProcessorJob( IXmlFileLoader fileLoader, IXmlValidator validator, ILogger<XmlProcessorJob> logger, IOptions<AppSettings> settings,IProcessedFileRepository repository)
        {
            _fileLoader = fileLoader;
            _validator = validator;
            _logger = logger;
            _repository = repository;

            var config = settings.Value;
            _inputFolder = config.InputFolderXML;
            _processingFolder = config.ProcessingFolderXML;
            _processedFolder = config.ProcessingFolderXML;
            _errorFolder = config.ErrorFolderXML;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("XmlProcessorJob started at {Time}", DateTimeOffset.Now);

            if (!Directory.Exists(_inputFolder))
            {
                _logger.LogWarning("Input folder '{Folder}' does not exist.", _inputFolder);
                return;
            }

            var files = Directory.GetFiles(_inputFolder, "*.xml");

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);

                var processingFilePath = Path.Combine(_processingFolder, fileName);

                try
                {
                    EnsureDirectoryExists(_processingFolder);

                    MoveFileSafely(file, processingFilePath);

                    _logger.LogInformation("Processing file: {File}", fileName);

                    var content = await _fileLoader.LoadAsync(processingFilePath);

                    var validationResult = _validator.Validate(content);

                    if (validationResult.IsValid)
                    {
                        _logger.LogInformation("Validation succeeded for file: {File}", fileName);

                        await MoveFileAsync(processingFilePath, _processedFolder);
                    }
                    else
                    {
                        _logger.LogWarning("Validation failed for file {File}", fileName);

                        foreach (var message in validationResult.Errors)
                        {
                            _logger.LogWarning("{Message}", message);
                        }

                        var processedFile = new ProcessedFile
                        {
                            PdfFileName = null,
                            XmlFileName = fileName,
                            GenerationDate = DateTime.Now,
                            Status = "Error",
                            FileSizeInBytes = new FileInfo(processingFilePath).Length,
                            PdfFilePath = null,
                            Errors = validationResult.Errors.Select(e => new XmlValidationError
                            {
                                LineNumber = e.LineNumber,
                                LinePosition = e.LinePosition,
                                Message = e.Message
                            }).ToList()
                        };

                        await _repository.AddAsync(processedFile);

                        await MoveFileAsync(processingFilePath, _errorFolder);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error processing file: {File}", fileName);
                    await MoveFileAsync(file, _errorFolder);
                }
            }
        }

        private static void EnsureDirectoryExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private static void MoveFileSafely(string sourcePath, string destinationPath)
        {
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }

            File.Move(sourcePath, destinationPath);
        }

        private async Task MoveFileAsync(string sourcePath, string targetFolder)
        {
            try
            {
                EnsureDirectoryExists(targetFolder);

                var fileName = Path.GetFileName(sourcePath);
                var destinationPath = Path.Combine(targetFolder, fileName);

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                await Task.Run(() => File.Move(sourcePath, destinationPath));

                _logger.LogInformation("Moved file {File} to folder {Folder}", fileName, targetFolder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to move file {File} to folder {Folder}", Path.GetFileName(sourcePath), targetFolder);
            }
        }
    }
}
