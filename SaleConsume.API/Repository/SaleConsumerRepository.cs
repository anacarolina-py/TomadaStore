using Microsoft.Extensions.Logging;
using TomadaStore.SaleAPI.Data;

namespace SaleConsume.API.Repository
{
    public class SaleConsumerRepository
    {
        private readonly ILogger<SaleConsumerRepository> _logger;
        private readonly ConnectionDB _connection;

        public SaleConsumerRepository(ILogger<SaleConsumerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;

        }
    }
}
