using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.E_CommerceDTOs;
using SharedData.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ShoppingCartController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await serviceManager.shoppingCartService.GetCartItemsAsync();
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("No items found in the cart.");
            }
            return Ok(ApiResponse<List<CartItemDto>>.SuccessResponse(cartItems, "Cart items retrieved successfully."));
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            if (productId <= 0 || quantity <= 0)
            {
                return BadRequest("Product ID and quantity must be greater than zero.");
            }
            await serviceManager.shoppingCartService.AddToCartAsync(productId , quantity);
            return Ok(ApiResponse<CartItemDto>.SuccessResponse(null, "Item added to cart successfully."));
        }
        [HttpDelete]
        public async Task<IActionResult> ClearCar()
        {
            await serviceManager.shoppingCartService.ClearCartAsync();
            return Ok(ApiResponse<string>.SuccessResponse(null, "Car deleted successfully."));
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("Product ID must be greater than zero.");
            }
            await serviceManager.shoppingCartService.RemoveItemAsync(productId);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Item removed from cart successfully."));
        }

        [HttpPut("{productId:int}")]
        public async Task<IActionResult> UpdateItemQuantity(int productId, int newQuantity)
        {
            if (productId <= 0 || newQuantity <= 0)
            {
                return BadRequest("Product ID and new quantity must be greater than zero.");
            }
            await serviceManager.shoppingCartService.UpdateItemQuantityAsync(productId, newQuantity);
            return Ok(ApiResponse<string>.SuccessResponse(null, "Item quantity updated successfully."));
        }

    }
}
