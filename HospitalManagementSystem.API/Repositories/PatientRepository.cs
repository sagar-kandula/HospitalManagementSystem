using HospitalManagementSystem.API.Controllers;
using HospitalManagementSystem.API.Data;
using HospitalManagementSystem.API.Models;
using Microsoft.Data.SqlClient;

namespace HospitalManagementSystem.API.Repositories
{
    public class PatientRepository
    {
        private readonly DbConnection _dbConnection;
        private object id;

        public PatientRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "SELECT * FROM Patients";

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Patient patient = new Patient();

                patient.PatientId = Convert.ToInt32(reader["PatientId"]);
                patient.Name = reader["Name"].ToString();
                patient.Age = Convert.ToInt32(reader["Age"]);
                patient.Gender = reader["Gender"].ToString();
                patient.Phone = reader["Phone"].ToString();
                patient.Address = reader["Address"].ToString();

                patients.Add(patient);
            }

            return patients;
        }
        public void UpdatePatient(Patient patient)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();
            connection.Open();

            string query = @"UPDATE Patients
                             SET Name = @Name,
                                 Age = @Age,
                                 Gender = @Gender,
                                 Phone = @Phone,
                                 Address = @Address
                                 WHERE PatientId = @PatientId";


            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientId", patient.PatientId);

            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Gender", patient.Gender);
            command.Parameters.AddWithValue("@Phone", patient.Phone);
            command.Parameters.AddWithValue("@Address", patient.Address);

            command.ExecuteNonQuery();

        }
        public void AddPatient(Patient patient)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = @"INSERT INTO Patients
                            (Name, Age, Gender, Phone, Address)
                            VALUES
                            (@Name, @Age, @Gender, @Phone, @Address)";


            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Gender", patient.Gender);
            command.Parameters.AddWithValue("@Phone", patient.Phone);
            command.Parameters.AddWithValue("@Address", patient.Address);

            command.ExecuteNonQuery();
        }
        public void DeletePatient(int id)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "DELETE FROM Patients WHERE PatientId = @PatientId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientId", id);

            command.ExecuteNonQuery();
        }

        public Patient GetPatientById(int id)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "SELECT * FROM Patients WHERE PatientId = @PatientId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientId", id);

            using SqlDataReader reader = command.ExecuteReader();

            if(reader.Read())
            {
                Patient patient = new Patient();

                patient.PatientId = Convert.ToInt32(reader["PatientId"]);

                patient.Name = reader["Name"].ToString();

                patient.Age = Convert.ToInt32(reader["Age"]);

                patient.Gender = reader["Gender"].ToString();

                patient.Phone = reader["Phone"].ToString();

                patient.Address = reader["Address"].ToString();

                return patient;



            }
            return null;

        }


           
      
    }
}