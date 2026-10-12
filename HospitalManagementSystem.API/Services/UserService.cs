using HospitalManagementSystem.API.DTOs;
using HospitalManagementSystem.API.Models;
using HospitalManagementSystem.API.Repositories;

namespace HospitalManagementSystem.API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;

        public UserService(
            UserRepository userRepository,
            PasswordService passwordService,
            TokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public void Register(RegisterDto registerDto)
        {
            if (_userRepository.EmailExists(registerDto.Email))
            {
                throw new Exception("Email is already registered.");
            }

            User user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = _passwordService.HashPassword(
                    registerDto.Password),
                Role = "User"
            };

            _userRepository.AddUser(user);
        }

        public LoginResponseDto Login(LoginDto loginDto)
        {
            User user = _userRepository.GetUserByEmail(
                loginDto.Email);

            if (user == null)
            {
                throw new Exception(
                    "Invalid email or password.");
            }

            bool isPasswordValid =
                _passwordService.VerifyPassword(
                    user.PasswordHash,
                    loginDto.Password);

            if (!isPasswordValid)
            {
                throw new Exception(
                    "Invalid email or password.");
            }

            string token = _tokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
