using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentController(AppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            
        }
        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            var appointments = _appointmentRepository.GetAllAppointments();

            return Ok(appointments);
        }
        [HttpPost]
        public IActionResult AddAppointment(Appointment appointment)
        {
            _appointmentRepository.AddAppointment(appointment);

            return Ok("Appointment added successfully!");
        }
        [HttpPut]
        public IActionResult UpdateAppointment(Appointment appointment)
        {
            _appointmentRepository.UpdateAppointment(appointment);

            return Ok("Appointment updated successfully!");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            _appointmentRepository.DeleteAppointment(id);

            return Ok("Appointment deleted successfully!");
        }
        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentById(id);

            return Ok(appointment);

        }

    }
}
