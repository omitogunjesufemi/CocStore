using Api.DTOs;
using Api.Models;
using Api.Repositories;
using Api.Services.Interfaces;
using Api.Services.Results;
using BCrypt.Net;
using System.Runtime.InteropServices;

namespace Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO?> AuthenticateUserAsync(UserLoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(loginDTO.Email);
            if (user == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                return null;

            var roles = user.Roles.Select(ur => ur.Name).ToList();
            var accessToken = _tokenService.GenerateAccessToken(user, roles, out string jwtId);
            var refreshToken = _tokenService.GenerateRefreshToken(jwtId, user.Id);

            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            var accessTokenExpiryMinutes = int.TryParse(_configuration["JwtSettings:AccessTokenExpirationMinutes"], out var val) ? val : 15;

            var responseDTO = new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(accessTokenExpiryMinutes)
            };
            return responseDTO;
        }

        public Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<StatusRecord> RegisterUserAsync(UserRegisterDTO registerDTO)
        {
            var userExists = await _unitOfWork.Users.EmailExistsAsync(registerDTO.Email);
            if (userExists)
                return new StatusRecord(StatusEnum.UserAlreadyExists, "Email already exists. Try another email address.");

            var user = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                IsActive = true
            };

            var role = await _unitOfWork.Users.GetRoleAsync("User");
            if (role != null)
                user.Roles.Add(role);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return new StatusRecord(StatusEnum.RegistrationComplete, "Registration was successful.");
        }

        public Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _unitOfWork.Users.GetUserByEmail(email);
        }
    }
}
