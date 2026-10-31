using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementation
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApiDbContext _dbContext;

        public OrderRepository(ApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Order>> GetOrdersWithOrderItems()
        {
            return await _dbContext.Set<Order>().AsNoTracking().Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
        }

        public async Task<Order?> GetOrderWithOrderItemsWT(Guid orderId)
        {
            return await _dbContext.Set<Order>().Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order?> GetOrderWithOrderItems(Guid orderId)
        {
            return await _dbContext.Set<Order>().AsNoTracking().Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
