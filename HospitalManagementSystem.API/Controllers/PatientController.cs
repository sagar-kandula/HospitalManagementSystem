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

            return Ok("Patient added successfully!");
        }

        [HttpPut]
        public IActionResult UpdatePatient(Patient patient)
        {
            _patientRepository.UpdatePatient(patient);

            return Ok("UPatient Updated SuccessFully");
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientById(int id)
        {
            var patient = _patientRepository.GetPatientById(id);

            return Ok(patient);
        }

    }
}