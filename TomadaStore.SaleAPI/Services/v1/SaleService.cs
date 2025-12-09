using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Repository.Interfaces;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Services.v1
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;
        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

       public SaleService(ILogger<SaleService> logger, 
           ISaleRepository saleRepository, 
           IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _saleRepository = saleRepository;
            _httpClientProduct = httpClientFactory.CreateClient("ProductAPI");
            _httpClientCustomer = httpClientFactory.CreateClient("CustomerAPI");

        }
       
        public async Task CreateSaleAsync(int idCustomer, SaleRequestDTO saleDto)
        {
               var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>($"api/v1/customer/get/{idCustomer}");

                if (customer == null)
                {
                    _logger.LogWarning($"Customer with ID {idCustomer} not found.");
                    throw new Exception("Customer not found.");
                }
                var products = new List<ProductResponseDTO>();
                decimal totalPrice = 0;

                foreach (var saleProduct in saleDto.Products)
                {
                    var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>
                        ($"api/v1/product/get/{saleProduct.ProductId}");

                    if (product == null)
                    {
                        _logger.LogWarning($"Product with ID {saleProduct.ProductId} not found.");
                        throw new Exception($"Product with ID {saleProduct.ProductId} not found.");
                    }

                    totalPrice += product.Price * saleProduct.Quantity;
                    products.Add(product);
                }
                await _saleRepository.CreateSaleAsync(customer, products, totalPrice);
           
        }
    }
}
