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
                var category = new Category(
                    product.Category.Name,
                    product.Category.Description
                    );
                var newProduct = new Product(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    category
                    );
                await _productCollection.InsertOneAsync(newProduct);
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
                var objectId = ObjectId.Parse(id);
                var deleteResult = await _productCollection.DeleteOneAsync(p => p.Id == objectId);

                if (deleteResult.DeletedCount == 0)
                {
                    _logger.LogWarning($"No product found with ID {id} to delete.");
                }
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

            return products.Select(product => new ProductResponseDTO
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
            }).ToList();

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
            try
            {
                var objectId = ObjectId.Parse(id);

                var category = new Category(
                    product.Category.Name,
                    product.Category.Description
                    );
                var updatedProduct = new Product(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    category
                    );
                var updateResult = await _productCollection.ReplaceOneAsync
                    (p => p.Id == objectId, updatedProduct);

                if (updateResult.MatchedCount == 0)
                {
                    _logger.LogWarning($"No product found with ID {id} to update.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
