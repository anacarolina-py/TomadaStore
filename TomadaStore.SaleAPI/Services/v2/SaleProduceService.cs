using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Repository.Interfaces;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Services.v2
{
    public class SaleProduceService
    {
        private readonly ConnectionFactory _factory;
        private readonly ILogger<SaleProduceService> _logger;
        

        public SaleProduceService(ILogger<SaleProduceService> logger)
        {
            _logger = logger;
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }
        public async Task ProduceSaleAsync(object sale)
        {
            string message = JsonSerializer.Serialize(sale);
            var body = Encoding.UTF8.GetBytes(message);

            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "sale_queue",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            await channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: "sale_queue",
                                            body: body);

            _logger.LogInformation($"[x] Sale enviada para fila.");
        }
    }
}
