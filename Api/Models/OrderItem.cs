using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Order))]
        [Required(ErrorMessage = "Order Id is required.")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey(nameof(Product))]
        [Required(ErrorMessage = "Product Id is required.")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 10000, ErrorMessage = "Quantity must be atleast 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, 99999999.99, ErrorMessage = "Unit Price must be between 0.01 and 99,999,999.99.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal UnitPrice { get; set; }
    }
}
