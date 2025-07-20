using IntravisionTest.DTO;
using IntravisionTest.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntravisionTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartPaymentController : ControllerBase
    {
        private readonly ICartPaymentService _cartPaymentService;

        public CartPaymentController(ICartPaymentService cartPaymentService)
        {
            _cartPaymentService = cartPaymentService;
        }

        [HttpPost]
        public async Task<IActionResult> PayCart([FromBody] CartPaymentRequestDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _cartPaymentService.PayAsync(dto, cancellationToken);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
