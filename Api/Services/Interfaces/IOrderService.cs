using Api.DTOs;
using Api.Models;
using Api.Services.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetOrderById(Guid id);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersWithItems();
        Task<Order?> CreateOrder(CUOrderDTO orderDTO);
        Task<StatusRecord> AddOrderItem(Guid orderId, CUOrderItemDTO orderItemDTO);
        Task<StatusRecord> UpdateOrder(Guid orderId, CUOrderDTO orderDTO);
        Task<StatusRecord> DeleteOrder(Guid orderId);
    }
}
