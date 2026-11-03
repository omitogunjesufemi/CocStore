using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;
using Api.Services.Results;

namespace Api.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<StatusRecord> AddCartItemItemToCart(Guid cartId, CUCartItemDTO cartItemDTO)
        {
            var cart = await _cartRepository.GetCartWithCartItemsWT(cartId);
            if (cart == null)
                return new StatusRecord(StatusEnum.CartNotFound, "Cart Not Found.");

            var product = await _productRepository.GetProductWithNoTracking(cartItemDTO.ProductId);
            if (product == null)
                return new StatusRecord(StatusEnum.InvalidProduct, "Product not found.");
            var productInItem = false;

            foreach (var item in cart.CartItems)
            {
                if (item.ProductId == cartItemDTO.ProductId)
                {
                    productInItem = true;
                    item.Quantity = cartItemDTO.Quantity;
                }
            }

            if (!productInItem)
            {
                var cartItem = new CartItem
                {
                    ProductId = cartItemDTO.ProductId,
                    Quantity = cartItemDTO.Quantity,
                };
                cart.CartItems.Add(cartItem);
            }
            _cartRepository.Update(cart);
            await _cartRepository.SaveAsync();
            return new StatusRecord(StatusEnum.Updated, "Item has been added to cart.");
        }

        public async Task<Cart?> CreateCart(CUCartDTO cartDTO)
        {
            var cart = new Cart
            {
                CartItems = new List<CartItem>()
            };

            foreach (var item in cartDTO.CartItems)
            {
                var product = await _productRepository.GetProductWithNoTracking(item.ProductId);
                if (product == null)
                    return null;

                var cartItem = new CartItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                cart.CartItems.Add(cartItem);
            }
            await _cartRepository.AddAsync(cart);
            await _cartRepository.SaveAsync();
            return cart;
        }

        public async Task<Cart?> GetCart(Guid cartId)
        {
            return await _cartRepository.GetCartWithCartItemsWNT(cartId);
        }

        public async Task<IEnumerable<Cart>> GetCarts()
        {
            return await _cartRepository.GetCartsWithCartItemsWNT();
        }
    }
}
