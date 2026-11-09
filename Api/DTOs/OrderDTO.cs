using Api.Models;
using Api.Services.Results;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Order Amount is required.")]
        public decimal OrderAmount { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public OrderStatus? Status { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }

    public class CUOrderDTO
    {
        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid order status.")]
        public OrderStatus? Status { get; set; }
        public List<CUOrderItemDTO> OrderItems { get; set; }
    }
}
