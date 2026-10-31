using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApiDbContext _dbContext;
        public ProductRepository(ApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
        {
            return await _dbContext.Set<Product>().AsNoTracking().Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductWithCategoryAsync(Guid id)
        {
            return await _dbContext.Set<Product>().AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetProductWithNoTracking(Guid id)
        {
            return await _dbContext.Set<Product>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
