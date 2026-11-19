using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Required(ErrorMessage = "Customer First Name is required.")]
        [StringLength(200, ErrorMessage = "Customer First Name cannot exceed 200 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer Last Name is required.")]
        [StringLength(200, ErrorMessage = "Customer Last Name cannot exceed 200 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country Code is required.")]
        public string CountryCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [ForeignKey(nameof(ShippingAddress))]
        public Guid? ShippingAddressId { get; set; }
        public Address? ShippingAddress { get; set; }

        [ForeignKey(nameof(User))]
        [Required(ErrorMessage = "User Id is required.")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
