using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Repository.Interfaces;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Services.v2
{
    public class SaleProduceService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleProduceService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

       public SaleProduceService(ILogger<SaleProduceService> logger, 
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
               var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCustomer.ToString());

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
                        (saleProduct.ProductId.ToString());

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
