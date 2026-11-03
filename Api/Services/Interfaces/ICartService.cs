using Api.DTOs;
using Api.Models;
using Api.Services.Results;

namespace Api.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> GetCart(Guid cartId);
        Task<IEnumerable<Cart>> GetCarts();
        Task<Cart?> CreateCart(CUCartDTO cartDTO);
        Task<StatusRecord> AddCartItemItemToCart(Guid cartId, CUCartItemDTO cartItemDTO);
    }
}
