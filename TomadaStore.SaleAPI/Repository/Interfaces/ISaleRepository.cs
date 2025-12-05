using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SaleAPI.Repository.Interfaces
{
    public interface ISaleRepository
    {
        Task CreateSaleAsync(CustomerResponseDTO customer, ProductResponseDTO product, SaleRequestDTO saleDto);
    }
}
