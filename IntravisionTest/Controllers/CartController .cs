using IntravisionTest.DTO;
using IntravisionTest.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntravisionTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartItemService _cartService;

        public CartController(ICartItemService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CartItemDTO>>> GetCart(CancellationToken cancellationToken)
        {
            var cartItems = await _cartService.GetCartAsync(cancellationToken);
            return Ok(cartItems);
        }

 
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddOrUpdateItem(int productId, [FromQuery] int quantity, CancellationToken cancellationToken)
        {
            await _cartService.AddOrUpdateItemAsync(productId, quantity, cancellationToken);
            return Ok();
        }


        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId, CancellationToken cancellationToken)
        {
            await _cartService.RemoveItemAsync(cartItemId, cancellationToken);
            return Ok();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
        {
            await _cartService.ClearCartAsync(cancellationToken);
            return Ok();
        }
    }
}
