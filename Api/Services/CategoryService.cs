using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;
using Api.Services.Results;

namespace Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CategoryExists(Guid id)
        {
            return await _categoryRepository.ExistAsync(id);
        }

        public async Task<Category> CreateCategory(CUCategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
            };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                return false;
            _categoryRepository.Delete(category);
            return true;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return null;
            return category;
        }

        public async Task<StatusRecord> UpdateCategory(Guid categoryId, CUCategoryDTO updateDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                return new StatusRecord(StatusEnum.CategoryNotFound, "The category does not exist.");

            category.Name = updateDTO.Name;
            category.Description = updateDTO.Description;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();
            return new StatusRecord(StatusEnum.Updated, "Category updated successfully.");
        }
    }
}
