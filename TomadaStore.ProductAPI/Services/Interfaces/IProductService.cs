using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductRequestDTO product);
        Task<ProductResponseDTO?> GetProductByIdAsync(string id);
        Task<List<ProductResponseDTO>> GetAllProductsAsync();
        Task UpdateProductAsync(string id, ProductRequestDTO product);
        Task DeleteProductAsync(string id);
    }
}
