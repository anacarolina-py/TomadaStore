using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.ProductAPI.Data;
using TomadaStore.ProductAPI.Repository.Interfaces;

namespace TomadaStore.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly ConnectionDB _connection;

        public ProductRepository(ILogger<ProductRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _productCollection = _connection.GetMongoCollection();
        }
        public async Task CreateProductAsync(ProductRequestDTO product)
        {
            try
            {
                await _productCollection.InsertOneAsync(new Product
                (
                   product.ToString(),
                   product.Name,
                   product.Description,
                   product.Price,
                   new Category
                   (
                       product.ToString(),
                       product.Category.Name,
                       product.Category.Description
                   )
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProductAsync(string id)
        {
            try
            {
                await _productCollection.DeleteOneAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
           var products = await _productCollection.Find(_ => true).ToListAsync();
            return products.ConvertAll(product => new ProductResponseDTO
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = new CategoryResponseDTO
                {
                    Id = product.Category.Id.ToString(),
                    Name = product.Category.Name,
                    Description = product.Category.Description
                }
            });

        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);

            var product = await _productCollection
                .Find(p => p.Id == objectId)
                .FirstOrDefaultAsync();

            if (product == null)
                return null;

            return new ProductResponseDTO
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = new CategoryResponseDTO
                {
                    Id = product.Category.Id.ToString(),
                    Name = product.Category.Name,
                    Description = product.Category.Description
                }
            };
        }


        public async Task UpdateProductAsync(string id, ProductRequestDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
