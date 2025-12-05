using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.Interfaces;

namespace TomadaStore.SaleAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;

        public SaleController(ILogger<SaleController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }
        [HttpPost("customer/{idCustomer}/product/{idProduct}")]
        public async Task<ActionResult> CreateSaleAsync(int idCustomer, string idProduct, [FromBody] SaleRequestDTO saleDto)
        {
            try
            {
                 _logger.LogInformation("Creating a new sale.");
                 await _saleService.CreateSaleAsync(idCustomer, idProduct, saleDto);
                 return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new sale." + ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
