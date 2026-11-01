using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.API.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<string> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<string>();
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword("HospitalUser", password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                "HospitalUser",
                hashedPassword,
                password
            );

            return result != PasswordVerificationResult.Failed;
        }
    }
}