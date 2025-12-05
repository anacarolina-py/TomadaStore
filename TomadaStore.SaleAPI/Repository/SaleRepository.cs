using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repository.Interfaces;

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
            ProductResponseDTO productDto, SaleRequestDTO salaeDto)
        {
            try
            {
                var products = new List<Product>();

                var category = new Category
                (
                    productDto.Category.ToString(),
                    productDto.Category.Name,
                    productDto.Category.Description
                );
                var product = new Product
                (
                    productDto.Id,
                    productDto.Name,
                    productDto.Description,
                    productDto.Price,
                    category
                );

                products.Add(product);

                var customer = new Customer
                (
                    customerDto.Id,
                    customerDto.FirstName,
                    customerDto.LastName,
                    customerDto.Email,
                    customerDto.PhoneNumber);

                await _saleCollection.InsertOneAsync(new Sale
                (
                   customer, 
                   products,
                   productDto.Price

                ));
            }
           catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}
