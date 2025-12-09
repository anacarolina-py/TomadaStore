using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SaleConsume.API.Service;
using SaleConsume.API.Service.Interfaces;
using System.Text;
using TomadaStore.Models.Models;

namespace SaleConsume.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleConsumerController : ControllerBase
    {
      private readonly ILogger<SaleConsumerController> _logger;
       private readonly  ISaleConsumerService _saleConsumerService;

        public SaleConsumerController(ILogger<SaleConsumerController> logger, ISaleConsumerService saleConsumerService)
        {
            _logger = logger;
            _saleConsumerService = saleConsumerService;
        }

        [HttpPost]
        public async Task<IActionResult> ConsumeSale()
        {
            await _saleConsumerService.NewSaleAsync();
            return Ok("Iniciando");
        }

       

    }
}
