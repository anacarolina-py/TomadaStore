using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.v1.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v1
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

        [HttpPost("{idCustomer}")]
        public async Task<ActionResult> CreateSaleAsync(int idCustomer, [FromBody] SaleRequestDTO saleDto)
        {
            try
            {
                 _logger.LogInformation("Creating a new sale.");
                 await _saleService.CreateSaleAsync(idCustomer, saleDto);
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
