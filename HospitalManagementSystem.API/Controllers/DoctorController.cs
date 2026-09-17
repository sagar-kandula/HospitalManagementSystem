using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;

        public DoctorController(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAllDoctors();

            return Ok(doctors);
        }
        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            _doctorRepository.AddDoctor(doctor);

            return Ok("Doctor added successfully!");
        }
        [HttpPut]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            _doctorRepository.UpdateDoctor(doctor);

            return Ok("Doctor Updated successfully!");
        }
        [HttpDelete("{id}")]
        public  IActionResult DeleteDoctor(int id)
        {
            _doctorRepository.DeleteDoctor(id);

            return Ok("Doctor deleted successfully!");
        }
        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            var doctor = _doctorRepository.GetDoctorById(id);

            return Ok(doctor);
        }

       
      
    }

}