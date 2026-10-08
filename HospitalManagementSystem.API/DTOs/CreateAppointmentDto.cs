using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.API.DTOs
{
    public class CreateAppointmentDto
    {

        [Range(1, int.MaxValue)]
        public int PatientId { get; set; }

        [Range(1, int.MaxValue)]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
