using Api.DTOs;
using Api.Services.Interfaces;
using Api.Services.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CUOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderService.CreateOrder(orderDTO);
            if (order == null)
                return BadRequest("Invalid product Id or order failed to be completed.");
            var dto = new OrderDTO
            {
                Id = order.Id,
                OrderAmount = order.OrderAmount,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                }).ToList()
            };
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersWithItems();
            var dtos = orders?.Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderAmount = o.OrderAmount,
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems?.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList() ?? new List<OrderItemDTO>()
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound("Order not found.");

            var dto = new OrderDTO
            {
                Id = order.Id,
                OrderAmount = order.OrderAmount,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList() ?? new List<OrderItemDTO>()
            };
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddItemToOrder(Guid id, [FromBody] CUOrderItemDTO orderItemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderService.AddOrderItem(id, orderItemDTO);
            if (result.Status == StatusEnum.OrderNotFound)
                return NotFound(result.Message);

            if (result.Status == StatusEnum.InvalidProduct)
                return BadRequest(result.Message);

            if (result.Status == StatusEnum.Updated)
                return NoContent();

            throw new InvalidOperationException("Internal Server Error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderService.DeleteOrder(id);
            if (result.Status == StatusEnum.OrderNotFound)
                return NotFound(result.Message);
            if (result.Status == StatusEnum.Deleted)
                return NoContent();
            throw new InvalidOperationException("Internal Server Error");
        }
    }
}
