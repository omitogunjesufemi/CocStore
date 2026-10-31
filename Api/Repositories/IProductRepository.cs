using Api.Models;

namespace Api.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetProductWithCategoryAsync(Guid id);

        Task<Product?> GetProductWithNoTracking(Guid id);
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
    }
}
