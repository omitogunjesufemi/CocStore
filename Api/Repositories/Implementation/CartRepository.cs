using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementation
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly ApiDbContext _dbContext;
        public CartRepository(ApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Cart>> GetCartsWithCartItemsWNT()
        {
            return await _dbContext.Set<Cart>().AsNoTracking().Include(c => c.CartItems).ThenInclude(ci => ci.Product).ToListAsync();
        }

        public async Task<Cart?> GetCartWithCartItemsWNT(Guid cartId)
        {
            return await _dbContext.Set<Cart>().Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public Task<Cart?> GetCartWithCartItemsWT(Guid cartId)
        {
            throw new NotImplementedException();
        }
    }
}
