using IntravisionTest.DTO;
using IntravisionTest.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntravisionTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CancellationToken cancellationToken)
        {
            var orderId = await _orderService.CreateOrderFromCartAsync(cancellationToken);
            return Ok(new { OrderId = orderId });
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllOrdersAsync(cancellationToken);
            return Ok(orders);
        }
    }
}
