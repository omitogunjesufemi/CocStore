using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [ForeignKey(nameof(Customer))]
        [Required(ErrorMessage = "Custoemr Id is required.")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<CartItem>? CartItems { get; set; }
    }
}
