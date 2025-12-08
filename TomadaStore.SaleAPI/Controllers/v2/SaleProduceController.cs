using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Net;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v2
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SaleProduceController : ControllerBase
    {
        private readonly ILogger<SaleProduceController> _logger;
        private readonly ISaleService _saleService;
        private readonly ConnectionFactory _connectionFactory;
        

        public SaleProduceController(ILogger<SaleProduceController> logger, ISaleService saleService, ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _saleService = saleService;
            _connectionFactory = new ConnectionFactory() { HostName = "localhost" }; 
            
        }
        [HttpPost("{idCustomer}")]
        public async Task<ActionResult> CreateSaleAsync(int idCustomer, [FromBody] SaleRequestDTO saleDto)
        {
            try
            {
                 _logger.LogInformation("Creating a new sale.");
                 await _saleService.CreateSaleAsync(idCustomer, saleDto);
                 return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new sale." + ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]

        public async Task<ActionResult> SaleProduceRequest([FromBody] SaleRequestDTO sale)
        {
            try
            {
                using var connection = await _connectionFactory.CreateConnectionAsync();

                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "sales_queue",
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

                var saleMessage = JsonSerializer.Serialize<SaleRequestDTO>(sale);

                var body = Encoding.UTF8.GetBytes(saleMessage);

                await channel.BasicPublishAsync(exchange: string.Empty,
                                                routingKey: "hello",
                                                body: body);

                _logger.LogInformation($" [x] Sent {sale}");

                return Ok("Message sent");

            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
