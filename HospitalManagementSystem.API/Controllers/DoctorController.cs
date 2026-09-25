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

            return StatusCode(201, "Doctor added successfully!");
        }
        [HttpPut]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            try
            {
                _doctorRepository.UpdateDoctor(doctor);

                return Ok("Doctor Updated successfully!");
            }
            catch(Exception ex)
            {
                if(ex.Message == "Doctor does not exist")
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
        public  IActionResult DeleteDoctor(int id)
        {
            try
            {

                _doctorRepository.DeleteDoctor(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                if(ex.Message == "Doctor does not exist")
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
        public IActionResult GetDoctor(int id)
        {
            var doctor = _doctorRepository.GetDoctorById(id);

            if(doctor == null)
            {
                return NotFound(new
                {
                    message = "Doctor not found."
                });
            }


            return Ok(doctor);
        }

       
      
    }

}