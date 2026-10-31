using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
