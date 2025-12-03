using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repository.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection.GetConnection();
        }
        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                var sql = "SELECT * FROM Customers";
                return (await _connection.QueryAsync<CustomerResponseDTO>(sql)).ToList();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error occurred while searching customer.");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while searching customer.");
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Customers WHERE Id = @Id";

                return await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sql, new { Id = id });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error occurred while searching customer.");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while searching customer.");
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            { 
                var sql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber)
                            VALUES (@FirstName, @LastName, @Email, @PhoneNumber);";

                await _connection.ExecuteAsync(sql, new
                {
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.PhoneNumber
                });

            }
            catch(SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error occurred while inserting customer.");
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while inserting customer.");
                throw new Exception(ex.StackTrace);
            }
        }
    }
}
