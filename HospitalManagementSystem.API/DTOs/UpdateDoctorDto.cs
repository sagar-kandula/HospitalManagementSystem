using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.API.DTOs
{
    public class UpdateDoctorDto
    {
        public int DoctorId { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Specialization { get; set; }

        [Range(0, 60)]
        public int Experience { get; set; }

        [Required] 
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set;}

    }
}
