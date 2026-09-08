using Microsoft.Data.SqlClient;

namespace HospitalManagementSystem.API.Data
{
    public class DbConnection
    {
        private readonly string _ConnectionString;

        public DbConnection(IConfiguration configuration)
        {
            _ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_ConnectionString);
        }
    }
}
