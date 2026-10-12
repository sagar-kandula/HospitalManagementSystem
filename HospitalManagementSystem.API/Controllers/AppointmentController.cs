using HospitalManagementSystem.API.DTOs;
using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            var appointments = _appointmentService.GetAllAppointments();

            List<AppointmentResponseDto> appointmentDtos =
                new List<AppointmentResponseDto>();

            foreach (var appointment in appointments)
            {
                AppointmentResponseDto appointmentDto =
                    new AppointmentResponseDto
                    {
                        AppointmentId = appointment.AppointmentId,
                        PatientId = appointment.PatientId,
                        DoctorId = appointment.DoctorId,
                        AppointmentDate = appointment.AppointmentDate,
                        AppointmentTime = appointment.AppointmentTime,
                        Status = appointment.Status
                    };

                appointmentDtos.Add(appointmentDto);
            }

            return Ok(appointmentDtos);
        }

        [HttpPost]
        public IActionResult AddAppointment(CreateAppointmentDto appointmentDto)
        {
            try
            {
                Appointment appointment = new Appointment
                {
                    PatientId = appointmentDto.PatientId,
                    DoctorId = appointmentDto.DoctorId,
                    AppointmentDate = appointmentDto.AppointmentDate,
                    AppointmentTime = appointmentDto.AppointmentTime,
                    Status = appointmentDto.Status
                };

                _appointmentService.AddAppointment(appointment);

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
        public IActionResult UpdateAppointment(UpdateAppointmentDto appointmentDto)
        {
            try
            {
                Appointment appointment = new Appointment
                {
                    AppointmentId = appointmentDto.AppointmentId,
                    PatientId = appointmentDto.PatientId,
                    DoctorId = appointmentDto.DoctorId,
                    AppointmentDate = appointmentDto.AppointmentDate,
                    AppointmentTime = appointmentDto.AppointmentTime,
                    Status = appointmentDto.Status
                };

                _appointmentService.UpdateAppointment(appointment);

                return Ok("Appointment updated successfully!");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Appointment does not exist.")
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
                _appointmentService.DeleteAppointment(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Appointment does not exist.")
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
            var appointment = _appointmentService.GetAppointmentById(id);

            if (appointment == null)
            {
                return NotFound(new
                {
                    message = "Appointment not found."
                });
            }

            AppointmentResponseDto appointmentDto =
                new AppointmentResponseDto
                {
                    AppointmentId = appointment.AppointmentId,
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Status = appointment.Status
                };

            return Ok(appointmentDto);
        }
    }
}