using Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DTOs
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Id is required.")]
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 10000, ErrorMessage = "Quantity must be atleast 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, 99999999.99, ErrorMessage = "Unit Price must be between 0.01 and 99,999,999.99.")]
        public decimal UnitPrice { get; set; }
    }

    public class CUOrderItemDTO
    {

        [Required(ErrorMessage = "Product Id is required.")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 10000, ErrorMessage = "Quantity must be atleast 1.")]
        public int Quantity { get; set; }
    }
}
