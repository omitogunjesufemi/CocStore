using Api.Models;

namespace Api.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetCartWithCartItemsWNT(Guid cartId);
        Task<Cart?> GetCartWithCartItemsWT(Guid cartId);
        Task<IEnumerable<Cart>> GetCartsWithCartItemsWNT();
    }
}
