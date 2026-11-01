using HospitalManagementSystem.API.Data;
using HospitalManagementSystem.API.Models;
using Microsoft.Data.SqlClient;

namespace HospitalManagementSystem.API.Repositories
{
    public class DoctorRepository
    {
        private readonly DbConnection _dbConnection;

        public DoctorRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();

            using SqlConnection connection = _dbConnection.CreateConnection();
            connection.Open();

            string query = "SELECT * FROM Doctors";

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Doctor doctor = new Doctor();

                doctor.DoctorId = Convert.ToInt32(reader["DoctorId"]);

                doctor.Name = reader["Name"].ToString();

                doctor.Specialization = reader["Specialization"].ToString();

                doctor.Experience = Convert.ToInt32(reader["Experience"]);

                doctor.Phone = reader["Phone"].ToString();

                doctor.Email = reader["Email"].ToString();

                doctors.Add(doctor);

            }
            return doctors;

        }
        public void AddDoctor(Doctor doctor)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();
            connection.Open();

            string query = @"INSERT INTO Doctors
                             (Name, Specialization, Experience, Phone, Email)
                              VALUES
                              (@Name, @Specialization, @Experience, @Phone, @Email)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
            command.Parameters.AddWithValue("@Experience", doctor.Experience);
            command.Parameters.AddWithValue("@Phone", doctor.Phone);
            command.Parameters.AddWithValue("@Email", doctor.Email);

            command.ExecuteNonQuery();
        }

        public void UpdateDoctor(Doctor doctor)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = @"UPDATE Doctors
                            SET Name = @Name,
                                Specialization = @Specialization,
                                Experience = @Experience,
                                Phone = Phone,
                                Email = @Email
                                WHERE DoctorId = @DoctorId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
            command.Parameters.AddWithValue("@Name", doctor.Name);
            command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
            command.Parameters.AddWithValue("@Experience", doctor.Experience);
            command.Parameters.AddWithValue("@Phone", doctor.Phone);
            command.Parameters.AddWithValue("@Email", doctor.Email);

            int rowsAffected = command.ExecuteNonQuery();
            if(rowsAffected == 0)
            {
                throw new Exception("Doctor does not exist.");
            }

        }
        public void DeleteDoctor(int id)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "DELETE FROM Doctors WHERE DoctorId = @DoctorId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorId", id);

            int rowsAffected = command.ExecuteNonQuery();
            if(rowsAffected == 0)
            {
                throw new Exception("Doctor does not exist.");
            }
        }
        public Doctor GetDoctorById(int id)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "SELECT * FROM Doctors WHERE DoctorId = @DoctorId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorId", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Doctor doctor = new Doctor();

                doctor.DoctorId = Convert.ToInt32(reader["DoctorId"]);

                doctor.Name = reader["Name"].ToString();

                doctor.Specialization = reader["Specialization"].ToString();

                doctor.Experience = Convert.ToInt32(reader["Experience"]);

                doctor.Phone = reader["Email"].ToString();

                doctor.Email = reader["Email"].ToString();

                return doctor;
            }

            return null;
        }
    }

}