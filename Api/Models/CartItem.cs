using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [ForeignKey(nameof(Cart))]
        [Required(ErrorMessage = "Cart Id is required.")]
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey(nameof(Product))]
        [Required(ErrorMessage = "Product Id is required.")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 10000, ErrorMessage = "Quantity must be atleast 1.")]
        public int Quantity { get; set; }
    }
}
