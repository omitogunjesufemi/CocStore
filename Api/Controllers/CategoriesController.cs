using Api.DTOs;
using Api.Models;
using Api.Services.Interfaces;
using Api.Services.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CUCategoryDTO createDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.CreateCategory(createDTO);
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return Ok(categoryDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            var dtos = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound("Category not found.");
            var dto = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody]CUCategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.UpdateCategory(id, categoryDTO);
            if (result.Status == StatusEnum.CategoryNotFound)
                return NotFound(result.Message);

            if (result.Status == StatusEnum.Updated)
                return NoContent();

            throw new InvalidOperationException("Internal server Error");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var exists = await _categoryService.CategoryExists(id);
            if (!exists)
                return NotFound("Category does not exist.");
            var isDeleted = await _categoryService.DeleteCategory(id);
            if (!isDeleted)
                return BadRequest("Category update failed.");
            return NoContent();
        }
    }
}
