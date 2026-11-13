using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementation
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ApiDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChangesAsync();
        }

        public void DeleteWithNoSaveAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dbSet.AnyAsync(entity => EF.Property<Guid>(entity, "Id") == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
