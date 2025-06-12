using LiteDB;
using XMLtoPDF.Domain.Entities;
using XMLtoPDF.Infrastructure.Interfaces;
using XMLtoPDF.Infrastructure.Persistence.XMLtoPDF.Infrastructure.Data;

namespace XMLtoPDF.Infrastructure.Persistence
{
    public class ProcessedFileRepository : IProcessedFileRepository
    {
        private readonly LiteDatabase _database;

        public ProcessedFileRepository(LiteDbContext context)
        {
            _database = context.Database;
        }

        public Task AddAsync(ProcessedFile file)
        {
            var col = _database.GetCollection<ProcessedFile>("processed_files");
            col.Insert(file);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ProcessedFile>> GetAllAsync()
        {
            var col = _database.GetCollection<ProcessedFile>("processed_files");
            return Task.FromResult(col.FindAll());
        }

        public Task<IEnumerable<ProcessedFile>> GetByStatusAsync(string status)
        {
            var col = _database.GetCollection<ProcessedFile>("processed_files");
            return Task.FromResult(col.Find(x => x.Status == status));
        }

        public Task<(IEnumerable<ProcessedFile> Results, int TotalCount)> GetPaginatedAsync(int page, int pageSize, string? status = null)
        {
            var col = _database.GetCollection<ProcessedFile>("processed_files");

            var query = string.IsNullOrEmpty(status)
                ? col.FindAll()
                : col.Find(x => x.Status == status);

            var totalCount = query.Count();
            var results = query
                .OrderByDescending(x => x.GenerationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return Task.FromResult((results, totalCount));
        }

        public Task<ProcessedFile?> GetByIdAsync(string id)
        {
            var col = _database.GetCollection<ProcessedFile>("processed_files");
            var result = col.FindById(new ObjectId(id));
            return Task.FromResult(result);
        }
    }
}
