using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Service;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> GetQueueAsync()
        {
            await _paymentService.GetQueueAsync();
            return Ok();
        }

        
    }
}
