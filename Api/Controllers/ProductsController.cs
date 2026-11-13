using Api.DTOs;
using Api.Services.Interfaces;
using Api.Services.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CUProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryExists = await _categoryService.CategoryExists(productDTO.CategoryId);
            if (!categoryExists)
                return BadRequest("You choose an invalid category id");
            
            var product = await _productService.CreateProduct(productDTO);

            var dto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                Price = product.Price,
                Quantity = product.Quantity,
            };
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAndTheirCategory();
            var dtos = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name,
                Quantity = p.Quantity,
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductWithCategory(id);
            if (product == null)
                return NotFound("Product not found.");

            var dto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                Quantity = product.Quantity
            };
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody]CUProductDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.UpdateProduct(id, updateDTO);
            if (result.Status == StatusEnum.ProductNotFound)
                return NotFound(result.Message);

            if (result.Status == StatusEnum.InvalidCategory)
                return BadRequest(result.Message);

            if (result.Status == StatusEnum.Updated)
                return NoContent();

            throw new InvalidOperationException("Internal server Error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result.Status == StatusEnum.ProductNotFound)
                return NotFound(result.Message);

            if (result.Status == StatusEnum.Deleted)
                return NoContent();

            throw new InvalidOperationException("Internal server Error");
        }
    }
}
