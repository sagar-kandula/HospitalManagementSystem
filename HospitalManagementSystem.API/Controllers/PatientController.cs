using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.DTOs;
using HospitalManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _patientService.GetAllPatients();

            List<PatientResponseDto> patientDtos = new List<PatientResponseDto>();

            foreach (var patient in patients)
            {
                PatientResponseDto patientDto = new PatientResponseDto
                {
                    PatientId = patient.PatientId,
                    Name = patient.Name,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Phone = patient.Phone,
                    Address = patient.Address
                };

                patientDtos.Add(patientDto);
            }

            return Ok(patientDtos);
        }

        [HttpPost]
        public IActionResult AddPatient(CreatePatientDto patientDto)
        {
            Patient patient = new Patient
            {
                Name = patientDto.Name,
                Age = patientDto.Age,
                Gender = patientDto.Gender,
                Phone = patientDto.Phone,
                Address = patientDto.Address
            };

            _patientService.AddPatient(patient);

            return StatusCode(201, "Patient added successfully!");
        }

        [HttpPut]
        public IActionResult UpdatePatient(UpdatePatientDto patientDto)
        {
            try
            {
                Patient patient = new Patient
                {
                    PatientId = patientDto.PatientId,
                    Name = patientDto.Name,
                    Age = patientDto.Age,
                    Gender = patientDto.Gender,
                    Phone = patientDto.Phone,
                    Address = patientDto.Address
                };

                _patientService.UpdatePatient(patient);

                return Ok("Patient Updated Successfully");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Patient does not exist.")
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                _patientService.DeletePatient(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Patient does not exist.")
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
        public IActionResult GetPatientById(int id)
        {
            var patient = _patientService.GetPatientById(id);

            if (patient == null)
            {
                return NotFound(new
                {
                    message = "Patient not found."
                });
            }

            PatientResponseDto patientDto = new PatientResponseDto
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                Age = patient.Age,
                Gender = patient.Gender,
                Phone = patient.Phone,
                Address = patient.Address
            };

            return Ok(patientDto);
        }
    }
}