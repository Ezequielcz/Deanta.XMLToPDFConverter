using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLtoPDF.Domain.Entities;

namespace XMLtoPDF.Infrastructure.Interfaces
{
    public interface IProcessedFileRepository
    {
        Task AddAsync(ProcessedFile file);
        Task<IEnumerable<ProcessedFile>> GetAllAsync();
        Task<IEnumerable<ProcessedFile>> GetByStatusAsync(string status);
        Task<(IEnumerable<ProcessedFile> Results, int TotalCount)> GetPaginatedAsync(int page, int pageSize, string? status = null);
        Task<ProcessedFile?> GetByIdAsync(string id);
    }
}
