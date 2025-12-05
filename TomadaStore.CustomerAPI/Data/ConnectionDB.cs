using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Models;
using TomadaStore.ProductAPI.Data;

namespace TomadaStore.CustomerAPI.Data

{
    public class ConnectionDB
    {
        private readonly string _connectionString;

        public ConnectionDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }


    }
}
