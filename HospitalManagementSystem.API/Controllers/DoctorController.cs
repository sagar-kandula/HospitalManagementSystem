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
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = _doctorService.GetAllDoctors();

            List<DoctorResponseDto> doctorDtos = new List<DoctorResponseDto>();

            foreach (var doctor in doctors)
            {
                DoctorResponseDto doctorDto = new DoctorResponseDto
                {
                    DoctorId = doctor.DoctorId,
                    Name = doctor.Name,
                    Specialization = doctor.Specialization,
                    Experience = doctor.Experience,
                    Phone = doctor.Phone,
                    Email = doctor.Email
                };

                doctorDtos.Add(doctorDto);
            }

            return Ok(doctorDtos);
        }

        [HttpPost]
        public IActionResult AddDoctor(CreateDoctorDto doctorDto)
        {
            Doctor doctor = new Doctor
            {
                Name = doctorDto.Name,
                Specialization = doctorDto.Specialization,
                Experience = doctorDto.Experience,
                Phone = doctorDto.Phone,
                Email = doctorDto.Email
            };

            _doctorService.AddDoctor(doctor);

            return StatusCode(201, "Doctor added successfully!");
        }

        [HttpPut]
        public IActionResult UpdateDoctor(UpdateDoctorDto doctorDto)
        {
            try
            {
                Doctor doctor = new Doctor
                {
                    DoctorId = doctorDto.DoctorId,
                    Name = doctorDto.Name,
                    Specialization = doctorDto.Specialization,
                    Experience = doctorDto.Experience,
                    Phone = doctorDto.Phone,
                    Email = doctorDto.Email
                };

                _doctorService.UpdateDoctor(doctor);

                return Ok("Doctor Updated successfully!");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Doctor does not exist.")
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
        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                _doctorService.DeleteDoctor(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Doctor does not exist.")
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
        public IActionResult GetDoctorById(int id)
        {
            var doctor = _doctorService.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound(new
                {
                    message = "Doctor not found."
                });
            }

            DoctorResponseDto doctorDto = new DoctorResponseDto
            {
                DoctorId = doctor.DoctorId,
                Name = doctor.Name,
                Specialization = doctor.Specialization,
                Experience = doctor.Experience,
                Phone = doctor.Phone,
                Email = doctor.Email
            };

            return Ok(doctorDto);
        }
    }
}