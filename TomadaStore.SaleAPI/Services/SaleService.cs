using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Repository.Interfaces;
using TomadaStore.SaleAPI.Services.Interfaces;

namespace TomadaStore.SaleAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;
        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

       public SaleService(ILogger<SaleService> logger, ISaleRepository saleRepository, HttpClient httpProduct, HttpClient httpCustomer)
        {
            _logger = logger;
            _saleRepository = saleRepository;
            _httpClientProduct = httpProduct;
            _httpClientCustomer = httpCustomer;

        }
       
        public async Task CreateSaleAsync(int idCustomer, string idProduct, SaleRequestDTO saleDto)
        {
            try
            {
               var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCustomer.ToString());

                if (customer == null)
                {
                    _logger.LogWarning($"Customer with ID {idCustomer} not found.");
                    throw new Exception("Customer not found.");
                }
                var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(idProduct);
               
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {idProduct} not found.");
                    throw new Exception("Product not found.");
                }
                await _saleRepository.CreateSaleAsync(customer, product, saleDto);

                _logger.LogInformation("Sale created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }
    }
}
