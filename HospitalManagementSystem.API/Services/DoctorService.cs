using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.Repositories;

namespace HospitalManagementSystem.API.Services
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        public DoctorService(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAllDoctors();
        }
        public Doctor GetDoctorById(int id)
        {
            return _doctorRepository.GetDoctorById(id);
        }
        public void AddDoctor(Doctor doctor)
        {
            _doctorRepository.AddDoctor(doctor);
        }
        public void UpdateDoctor(Doctor doctor)
        {
            _doctorRepository.UpdateDoctor(doctor);
        }
        public void DeleteDoctor(int id)
        {
            _doctorRepository.DeleteDoctor(id);
        }

    }
}
