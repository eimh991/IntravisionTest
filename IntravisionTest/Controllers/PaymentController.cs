using IntravisionTest.DTO;
using IntravisionTest.Interface;
using IntravisionTest.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntravisionTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServicecs _paymentService;

        public PaymentController(IPaymentServicecs paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<ChangeResponseDTO>> Pay(PaymentRequestDTO request, CancellationToken cancellationToken)
        {
            var change = await _paymentService.PayOrderAsync(request, cancellationToken);
            return Ok(change);
        }
    }
}
