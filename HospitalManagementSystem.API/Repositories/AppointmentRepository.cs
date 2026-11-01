using HospitalManagementSystem.API.Models;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.API.Data;

namespace HospitalManagementSystem.API.Repositories
{
    public class AppointmentRepository
    {
        private readonly DbConnection _dbConnection;
        private object appointment;

        public AppointmentRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;

        }
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();

            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "SELECT * FROM Appointments";

            using SqlCommand command = new(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Appointment appointment = new Appointment();

                appointment.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);
                appointment.PatientId = Convert.ToInt32(reader["PatientId"]);
                appointment.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                appointment.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                appointment.AppointmentTime = (TimeSpan)reader["AppointmentTime"];
                appointment.Status = reader["Status"].ToString();

                appointments.Add(appointment);

            }
            return appointments;

        }
        public void AddAppointment(Appointment appointment)
        {
            if (!PatientExists(appointment.PatientId))
            {
                throw new Exception("Patient does not exist.");

            }
            if(!DoctorExists(appointment.DoctorId))
            {
                throw new Exception("Doctor does not exist.");
            }

            try
            {

                using SqlConnection connection = _dbConnection.CreateConnection();

                connection.Open();

                string query = @"INSERT INTO Appointments
                               (PatientId, DoctorId, AppointmentDate, AppointmentTime, Status)
                               VALUES
                               (@PatientId, @DoctorId, @AppointmentDate, @AppointmentTime, @Status)";


                using SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                command.Parameters.AddWithValue("@Status", appointment.Status);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to add appointment. Please check PatientId and DoctorId.", ex);
            }

        }
        public void UpdateAppointment(Appointment appointment)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = @"UPDATE Appointments
                           SET PatientId = @PatientId,
                           DoctorId = @DoctorId,
                           AppointmentDate = @AppointmentDate,
                           AppointmentTime = @AppointmentTime,
                           Status = @Status
                           WHERE AppointmentId = @AppointmentId";


            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);
            command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
            command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
            command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
            command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
            command.Parameters.AddWithValue("@Status", appointment.Status);

            int rowsAffected = command.ExecuteNonQuery();

            if(rowsAffected == 0)
            {
                throw new Exception("Appointment does not exist.");
            }

        }
        public void DeleteAppointment(int id)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentId", id);

            int rowsAffected = command.ExecuteNonQuery();
            
            if(rowsAffected == 0)
            {
                throw new Exception("Appointment does not exist.");
            }

        }
        public Appointment GetAppointmentById(int id)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "SELECT *  FROM Appointments WHERE AppointmentId = @AppointmentId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentId", id);

            using SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Appointment appointment = new Appointment();

                appointment.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);

                appointment.PatientId = Convert.ToInt32(reader["PatientId"]);

                appointment.DoctorId = Convert.ToInt32(reader["DoctorId"]);

                appointment.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);

                appointment.AppointmentTime = (TimeSpan)reader["AppointmentTime"];

                appointment.Status = reader["Status"].ToString();

                return appointment;
            }
            return null;

        }
        private bool PatientExists(int patientId)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "SELECT COUNT(1) FROM Patients Where PatientId = @PatientId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientId", patientId);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }
        private bool DoctorExists(int doctorId)
        {
            using SqlConnection connection = _dbConnection.CreateConnection();

            connection.Open();

            string query = "SELECT COUNT(1) FROM Doctors Where DoctorId = @DoctorId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorId", doctorId);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }

    }
}
