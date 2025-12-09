using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductAPI.Repository.Interfaces;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;
        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task CreateProductAsync(ProductRequestDTO product)
        {
            try
            {
                await _productRepository.CreateProductAsync(product);

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
                await _productRepository.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            try
            {
                 _logger.LogInformation("Fetching all products from the repository.");
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching products: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductResponseDTO?> GetProductByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Fetching product with ID {id} from the repository.");
                return await _productRepository.GetProductByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching product with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateProductAsync(string id, ProductRequestDTO product)
        {
            try
            {
                 await _productRepository.UpdateProductAsync(id, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
