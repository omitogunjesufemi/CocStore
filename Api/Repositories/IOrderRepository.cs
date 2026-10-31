using Api.Models;

namespace Api.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersWithOrderItems();
        Task<Order?> GetOrderWithOrderItems(Guid orderId);
        Task<Order?> GetOrderWithOrderItemsWT(Guid orderId);
    }
}
