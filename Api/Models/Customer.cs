using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country Code is required.")]
        public string CountryCode { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
