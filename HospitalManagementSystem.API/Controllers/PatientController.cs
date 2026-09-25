using HospitalManagementSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.API.Models;

namespace HospitalManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientRepository _patientRepository;

        public PatientController(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _patientRepository.GetAllPatients();

            return Ok(patients);
        }

        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {
            _patientRepository.AddPatient(patient);

            return StatusCode(201,"Patient added successfully!");
        }

        [HttpPut]
        public IActionResult UpdatePatient(Patient patient)
        {
            try
            {
                _patientRepository.UpdatePatient(patient);

                return Ok("Patient Updated Successfully");
            }
            catch(Exception ex)
            {
                if(ex.Message == "Patient does not exist.")
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
        public IActionResult DeletePatient(int id)
        {
            try
            {
                _patientRepository.DeletePatient(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                if (ex.Message == "Patient does not exist.") ;
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
            var patient = _patientRepository.GetPatientById(id);

            if(patient == null)
            {
                return NotFound(new
                {
                    message = "Patient not found."
                });
            }

            return Ok(patient);
        }

    }
}