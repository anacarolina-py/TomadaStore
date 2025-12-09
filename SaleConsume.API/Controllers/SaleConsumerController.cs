using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SaleConsume.API.Service;
using System.Text;

namespace SaleConsume.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleConsumerController : ControllerBase
    {
      private readonly ILogger<SaleConsumerController> _logger;
       private readonly  SaleConsumerService _saleConsumerService;

        public SaleConsumerController(ILogger<SaleConsumerController> logger, SaleConsumerService saleConsumerService)
        {
            _logger = logger;
            _saleConsumerService = saleConsumerService;
        }

        [HttpPost]
        public async Task<IActionResult> ConsumeSale()
        {
            _ = _saleConsumerService.NewSaleAsync();
            return Ok("Iniciando");
        }
       

    }
}
