using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;

        public OrderItemService (IRepository<OrderItem> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItem?> GetOrderItemById(Guid id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
                return null;
            return orderItem;
        }
    }
}
