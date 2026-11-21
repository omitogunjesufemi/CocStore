using Api.DTOs;
using Api.Models;
using Api.Services;
using Api.Services.Interfaces;
using Api.Services.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody]CUCartDTO cartDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
                return Unauthorized("Kindly login to add item to cart.");

            var cartCreated = await _cartService.CreateCart(cartDTO, userEmail);
            if (cartCreated == null)
                return BadRequest("Invalid Product.");

            var cart = await _cartService.GetCart(cartCreated.Id);
            if (cart == null)
                return BadRequest("Invalid Product.");

            var dto = DTOConverter(cart);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            var carts = await _cartService.GetCarts();
            var dtos = carts?.Select(c => DTOConverter(c));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(Guid id)
        {
            var cart = await _cartService.GetCart(id);
            if (cart == null)
                return NotFound("Cart not found.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = DTOConverter(cart);
            return Ok(dto);
        }

        [HttpPut("{id}/additem")]
        public async Task<IActionResult> AddItemToCart(Guid id, CUCartItemDTO cartItemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartService.AddCartItemItemToCart(id, cartItemDTO);
            if (result.Status == StatusEnum.CartNotFound)
                return NotFound(result.Message);

            if (result.Status == StatusEnum.InvalidProduct)
                return BadRequest(result.Message);

            if (result.Status == StatusEnum.Updated)
                return NoContent();

            throw new InvalidOperationException("Internal Server Error");
        }

        [HttpPut("{cartId}/removeitem/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid cartId, Guid productId)
        {
            var result = await _cartService.RemoveItemFromCart(cartId, productId);
            if (result.Status == StatusEnum.CartNotFound)
                return NotFound(result.Message);

            if (result.Status == StatusEnum.InvalidProduct)
                return BadRequest(result.Message);

            if (result.Status == StatusEnum.Updated)
                return NoContent();

            throw new InvalidOperationException("Internal Server Error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var result = await _cartService.DeleteCart(id);
            if (result.Status == StatusEnum.CartNotFound)
                return NotFound(result.Message);

            if (result.Status == StatusEnum.Deleted)
                return NoContent();

            throw new InvalidOperationException("Internal Server Error");
        }

        private CartDTO DTOConverter(Cart cart)
        {
            return new CartDTO
            {
                Id = cart.Id,
                CustomerId = cart.CustomerId,
                TotalAmount = cart.CartItems.Sum(i => i.Quantity * i.Product.Price),
                CartItems = cart.CartItems.Select(ci => new CartItemDTO
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                }).ToList()
            };
        }
    }
}
