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
      private readonly ConnectionFactory _connectionFactory;
       private readonly  SaleConsumerService _saleConsumerService;

        public SaleConsumerController(ILogger<SaleConsumerController> logger, ConnectionFactory connectionFactory, SaleConsumerService saleConsumerService)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _saleConsumerService = saleConsumerService;
        }

        [HttpPost]

        public async Task<IActionResult> ConsumeSale()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "sales_queue",
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);


                await channel.BasicPublishAsync(exchange: string.Empty,
                                       routingKey: "hello",
                                       body: body);

                _logger.LogInformation($" [x] Received {consumer}");
             
            };

            return Ok("Message received");
        }

    }
}
