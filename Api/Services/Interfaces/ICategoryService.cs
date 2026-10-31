using Api.DTOs;
using Api.Models;
using Api.Services.Results;

namespace Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category?> GetCategoryById(Guid id);
        Task<Category> CreateCategory(CUCategoryDTO categoryDTO);
        Task<StatusRecord> UpdateCategory(Guid categoryId, CUCategoryDTO updateDTO);
        Task<bool> DeleteCategory(Guid categoryid);
        Task<bool> CategoryExists(Guid id);
    }
}
