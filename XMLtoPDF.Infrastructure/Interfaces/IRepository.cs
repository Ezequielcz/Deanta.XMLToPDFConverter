namespace XMLtoPDF.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T item);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }

}
