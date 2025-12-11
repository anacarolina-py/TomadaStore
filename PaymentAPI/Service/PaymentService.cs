using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Payment;
using TomadaStore.Models.DTOs.Payments;

namespace PaymentAPI.Service
{
    public class PaymentService
    {
        private readonly ConnectionFactory _factory;
        private readonly ILogger<PaymentService> _logger;


        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        public async Task GetQueueAsync()
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

                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var sale = JsonSerializer.Deserialize<PaymentSaleMessageDTO>(message);
                _logger.LogInformation($"[x] Received {message}");

                var result = ProcessPayment(sale);

                 await ProcessedPayment(result);

            };

            await channel.BasicConsumeAsync(queue: "sale_queue",
                                            autoAck: true,
                                            consumer: consumer);

        }
        private PaymentResultMessageDTO ProcessPayment(PaymentSaleMessageDTO sale)
        {
            string status = sale.TotalPrice <= 1000 ? "Approved" : "Denied";

            return new PaymentResultMessageDTO
            {
                Customer = sale.Customer,
                Products = sale.Products,
                TotalPrice = sale.TotalPrice,
                Status = status
            };
        }

        public async Task ProcessedPayment(PaymentResultMessageDTO result)
        {
           
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "saleprocessed_queue",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var message = JsonSerializer.Serialize(result);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: "saleprocessed_queue",
                                            body: body);

            _logger.LogInformation($"[x] Sale enviada para fila.");
        }
    }
    }

