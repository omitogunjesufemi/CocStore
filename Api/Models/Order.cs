using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate {  get; set; }

        [Required(ErrorMessage = "Order Amount is required.")]
        [Range(0.00, 99999999.99, ErrorMessage = "Unit Price must be between 0.00 and 99,999,999.99.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal OrderAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
