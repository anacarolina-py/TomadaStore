using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Getting all products.");
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all products." + ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductRequestDTO product)
        {
            try
            {
                _logger.LogInformation("Creating a new product.");
                await _productService.CreateProductAsync(product);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new product." + ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(string id)
        {
            try
            {
                _logger.LogInformation($"Getting product with ID: {id}");
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting product with ID: {id}. " + ex.Message);
                return Problem(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteProduct(string id)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Deleting product with ID: {id}");
        //        var result = await _productService.DeleteProductAsync(id);
        //        if (!result)
        //        {
        //            return NotFound();
        //        }
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error occurred while deleting product with ID: {id}. " + ex.Message);
        //        return Problem(ex.Message);
        //    }
        //}
    }
}
