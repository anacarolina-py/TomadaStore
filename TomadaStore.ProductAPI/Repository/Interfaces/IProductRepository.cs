using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(ProductRequestDTO product);
        Task<ProductResponseDTO?> GetProductByIdAsync(string id);
        Task<List<ProductResponseDTO>> GetAllProductsAsync();
        Task UpdateProductAsync(string id, ProductRequestDTO product);
        Task DeleteProductAsync(string id);
    }
}
