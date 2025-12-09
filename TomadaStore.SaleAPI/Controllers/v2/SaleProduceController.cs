using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Net;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.v1.Interfaces;
using TomadaStore.SaleAPI.Services.v2;

namespace TomadaStore.SaleAPI.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SaleProduceController : ControllerBase
    {
        private readonly ILogger<SaleProduceController> _logger;
        private readonly SaleProduceService _saleService;
        

        public SaleProduceController(ILogger<SaleProduceController> logger, SaleProduceService saleService)
        {
            _logger = logger;
            _saleService = saleService;
      
        }

        [HttpPost]
        public async Task<IActionResult> ProduceSale(SaleProduceService saleProduce)
        {
            await _saleService.ProduceSaleAsync(saleProduce);
            return Ok("Sale enviada para fila.");
        }
    }
}
