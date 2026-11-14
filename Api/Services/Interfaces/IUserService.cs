using Api.DTOs;
using Api.Models;
using Api.Services.Results;

namespace Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<StatusRecord> RegisterUserAsync(UserRegisterDTO registerDTO);
        Task<AuthResponseDTO?> AuthenticateUserAsync(UserLoginDTO loginDTO);
        Task<AuthResponseDTO?> RefreshTokenAsync(string refreshToken);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
        Task<User?> GetUserByEmail(string email);
    }
}
