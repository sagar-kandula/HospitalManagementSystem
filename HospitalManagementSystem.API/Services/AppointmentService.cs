using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.Repositories;

namespace HospitalManagementSystem.API.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentService(AppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public List<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAllAppointments();
        }
        public Appointment GetAppointmentById(int id)
        {
            return _appointmentRepository.GetAppointmentById(id);
        }
        public void AddAppointment(Appointment appointment)
        {
            _appointmentRepository.AddAppointment(appointment);
        }
        public void UpdateAppointment(Appointment appointment)
        {
            _appointmentRepository.UpdateAppointment(appointment);
        }
        public void DeleteAppointment(int id)
        {
            _appointmentRepository.DeleteAppointment(id);
        }
    }
}
