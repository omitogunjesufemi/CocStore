using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class CustomerDTO
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }       
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? Email { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }

    public class CUCustomerDTO
    {
        [Required(ErrorMessage = "User Id is required.")]
        public Guid UserId { get; set; }

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
    }
}
