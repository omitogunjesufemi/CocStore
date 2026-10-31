using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(150, ErrorMessage = "Product Name cannot exceed 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Product Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Category))]
        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
