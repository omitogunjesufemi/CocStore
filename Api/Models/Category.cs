using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(150, ErrorMessage = "Category name cannot exceed 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Category Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
