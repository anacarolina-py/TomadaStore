using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repository.Interfaces;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger;
        private readonly IMongoCollection<Sale> _saleCollection;
        private readonly ConnectionDB _connection;

        public SaleRepository(ILogger<SaleRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _saleCollection = _connection.GetMongoCollection();
        }

        public async Task CreateSaleAsync(CustomerResponseDTO customerDto, 
            List<ProductResponseDTO> productDto, decimal total)
        {
            try
            {
                var customer = new Customer(
                    customerDto.Id,
                    customerDto.FirstName,
                    customerDto.LastName,
                    customerDto.Email,
                    customerDto.PhoneNumber
                );

                var products = productDto.Select(p => new Product(
                    new ObjectId( p.Id ),
                    p.Name,
                    p.Description,
                    p.Price,
                    new Category(
                        p.Category.Name,
                        p.Category.Description)

                )).ToList();

                var sale = new Sale(
                    customer,
                    products,
                    total
                );
                await _saleCollection.InsertOneAsync(sale);

            }
           catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}
