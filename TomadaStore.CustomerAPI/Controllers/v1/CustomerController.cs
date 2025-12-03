using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.CustomerAPI.Services;
using TomadaStore.CustomerAPI.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerRequestDTO customer)
        {
            try
            {
                _logger.LogInformation("Creating a new costumer.");

                await _customerService.InsertCustomerAsync(customer);

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new customer." + ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerById(int id)
        {
            try
            {
                _logger.LogInformation("Getting customer by id.");
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer by id." + ex.Message);
                return Problem(ex.Message);
            }
        }

          [HttpGet("getAll")]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomers()
        {
            try
            {
                _logger.LogInformation("Getting all customers.");
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all customers." + ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}