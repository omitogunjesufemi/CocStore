namespace Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWithNoSaveAsync(T entity);
        Task<bool> ExistAsync(Guid id);
        Task SaveAsync();
    }
}
