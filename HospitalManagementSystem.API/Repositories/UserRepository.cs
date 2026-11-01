using HospitalManagementSystem.API.Data;
using HospitalManagementSystem.API.Models;
using Microsoft.Data.SqlClient;


namespace HospitalManagementSystem.API.Repositories
{
    public class UserRepository
    {
        private readonly DbConnection _dbConnection;
        public UserRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public bool EmailExists(string email)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();
            connection.Open();

            string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Email", email);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }
        public void AddUser(User user)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();
            connection.Open();
            string query = @"
                            INSERT INTO Users (Name, Email, PasswordHash, Role)
                            VALUES (@Name, @Email, @PasswordHash, @Role)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@Role", user.Role);

            command.ExecuteNonQuery();
        }
        public User GetUserByEmail(string email)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();
            connection.Open();

            string query = @"SELECT UserId, Name, Email, PasswordHash, Role
                             FROM Users
                             WHERE Email = @Email";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    UserId = Convert.ToInt32(reader["UserId"]),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    PasswordHash = reader["PasswordHash"].ToString(),
                    Role = reader["Role"].ToString()
                };
            }

            return null;

        }
    }
}
