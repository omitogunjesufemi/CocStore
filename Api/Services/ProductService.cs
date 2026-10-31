using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;
using Api.Services.Results;

namespace Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductService(IProductRepository productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Product> CreateProduct(CUProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                CategoryId = productDTO.CategoryId,
                Price = productDTO.Price,
            };
            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            return product;
        }

        public async Task<StatusRecord> DeleteProduct(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return new StatusRecord(StatusEnum.ProductNotFound, "The product does not exist.");
            _productRepository.Delete(product);
            return new StatusRecord(StatusEnum.Deleted, "Product deleted successfully.");
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);

        }

        public async Task<Product?> GetProductWithCategory(Guid id)
        {
            return await _productRepository.GetProductWithCategoryAsync(id);

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAndTheirCategory()
        {
            return await _productRepository.GetProductsWithCategoryAsync();
        }

        public async Task<bool> ProductExists(Guid id)
        {
            return await _productRepository.ExistAsync(id);
        }

        public async Task<StatusRecord> UpdateProduct(Guid productId, CUProductDTO updateDTO)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return new StatusRecord(StatusEnum.ProductNotFound, "The product does not exist.");

            var categoryExists = await _categoryRepository.ExistAsync(updateDTO.CategoryId);
            if (!categoryExists)
                return new StatusRecord(StatusEnum.InvalidCategory, "The selected category does not exist."); ;

            product.Name = updateDTO.Name;
            product.Description = updateDTO.Description;
            product.CategoryId = updateDTO.CategoryId;
            product.Price = updateDTO.Price;
            _productRepository.Update(product);
            await _productRepository.SaveAsync();
            return new StatusRecord(StatusEnum.Updated, "Product updated successfully.");
        }
    }
}
