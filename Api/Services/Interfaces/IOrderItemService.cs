using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItem?> GetOrderItemById(Guid id); 
    }
}
