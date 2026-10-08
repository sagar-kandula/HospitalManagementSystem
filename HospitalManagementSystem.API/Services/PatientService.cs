using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.Repositories;

namespace HospitalManagementSystem.API.Services
{
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public List<Patient> GetAllPatients()
        {
            return _patientRepository.GetAllPatients();
        }
        public Patient GetPatientById(int id)
        {
            return _patientRepository.GetPatientById(id);
        }
        public void AddPatient(Patient patient)
        {
            _patientRepository.AddPatient(patient);
        }
        public void UpdatePatient(Patient patient)
        {
            _patientRepository.UpdatePatient(patient);
        }
        public void DeletePatient(int id)
        {
            _patientRepository.DeletePatient(id);
        }
    }
}
