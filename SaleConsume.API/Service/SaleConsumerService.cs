using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SaleConsume.API.Repository;
using SaleConsume.API.Repository.Interfaces;
using SaleConsume.API.Service.Interfaces;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace SaleConsume.API.Service
{
    public class SaleConsumerService : ISaleConsumerService
    {
        private readonly ISaleConsumerRepository _repository;
        private readonly ConnectionFactory _factory;
        private readonly ILogger<SaleConsumerService> _logger;
        public SaleConsumerService(ISaleConsumerRepository repository, ILogger<SaleConsumerService> logger)
        {
            _repository = repository;
            _logger = logger;
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

        }

        public async Task NewSaleAsync()
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "sale_queue",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            _logger.LogInformation("[*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    _logger.LogInformation($"[x] Received {message}");

                    var sale = JsonSerializer.Deserialize<Sale>(message);

                    if (sale == null)
                    {
                        _logger.LogInformation("Erro ao deserializar");
                        return;
                    }

                    await _repository.SaveSaleAsync(sale);
                    _logger.LogInformation("Sale salva no banco.");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Erro ao processar mensagem {ex.Message}");
                }
            };
            await channel.BasicConsumeAsync(queue: "sale_queue",
                                            autoAck: true,
                                            consumer: consumer);
        

        }
    } 
}
