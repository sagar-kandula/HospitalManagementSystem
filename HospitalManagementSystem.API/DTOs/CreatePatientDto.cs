using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.API.DTOs
{
    public class CreatePatientDto
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
