using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Data;

namespace SaleConsume.API.Repository
{
    public class SaleConsumerRepository
    {
        private readonly ILogger<SaleConsumerRepository> _logger;
        private readonly ConnectionDB _connection;
        private readonly IMongoCollection<Sale> _collection;

        public SaleConsumerRepository(ILogger<SaleConsumerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _collection = connection.GetMongoCollection();

        }

        public async Task SaveSaleAsync(Sale sale)
        {
            await _collection.InsertOneAsync(sale);
        }
    }
}
