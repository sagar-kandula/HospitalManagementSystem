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
            try
            {
                _appointmentRepository.AddAppointment(appointment);

                return StatusCode(201, "Appointment added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });

            }
            
        }
        [HttpPut]
        public IActionResult UpdateAppointment(Appointment appointment)
        {
            try
            {
                _appointmentRepository.UpdateAppointment(appointment);

                return Ok("Appointment updated successfully!");
            }
            catch(Exception ex)
            {
                if (ex.Message == "Appointment does not exist.") ;
                {
                    return NotFound(new
                    {
                        message = ex.Message

                    });

                }
                return BadRequest(new
                {
                    message = ex.Message
                });

            }
            
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            try
            {

                _appointmentRepository.DeleteAppointment(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                if(ex.Message == "Appointment does not exist.")
                {
                    return NotFound(new
                    {
                        message = ex.Message
                    });
                }
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            var appointment = _appointmentRepository.GetAppointmentById(id);

            if(appointment == null)
            {
                return NotFound(new
                {
                    message = "Appointment not fount."
                });
            }

            return Ok(appointment);

        }

    }
}
