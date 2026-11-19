using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Street Address is required.")]
        public string StreetAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]

        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]

        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        public string Country {  get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;
    }
}
