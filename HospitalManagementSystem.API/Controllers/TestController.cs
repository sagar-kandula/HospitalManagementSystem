using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.API.Data;

namespace HospitalManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DbConnection _dbConnection;

        public TestController(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public IActionResult TestConnection()
        {
            try
            {
                using SqlConnection connection = _dbConnection.CreateConnection();

                connection.Open();

                return Ok("Database connection successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}