using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(150, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }

    public class CUCategoryDTO
    {
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(150, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }
}
