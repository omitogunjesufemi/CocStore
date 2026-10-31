using Api.DTOs;
using Api.Models;
using Api.Services.Results;

namespace Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsAndTheirCategory();
        Task<Product?> GetProductById(Guid id);
        Task<Product?> GetProductWithCategory(Guid id);
        Task<Product> CreateProduct(CUProductDTO productDTO);
        Task<StatusRecord> UpdateProduct(Guid productId, CUProductDTO updateDTO);
        Task<StatusRecord> DeleteProduct(Guid productId);
        Task<bool> ProductExists(Guid id);
    }
}
