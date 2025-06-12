using XMLtoPDF.Application.Interfaces;

namespace XMLtoPDF.Application.Services
{
    public class XmlToInDesignXmlConverterService : IXmlToInDesignXmlConverterService
    {
        public async Task ConvertAsync(string sourceXmlFilePath, string outputXmlFilePath)
        {
            var xmlContent = await File.ReadAllTextAsync(sourceXmlFilePath);

            var inDesignXml = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                              $"<!-- Converted for InDesign -->\n" +
                              $"<InDesignDocument>\n" +
                              $"{xmlContent}\n" +
                              $"</InDesignDocument>";

            await File.WriteAllTextAsync(outputXmlFilePath, inDesignXml);
        }
    }
}
