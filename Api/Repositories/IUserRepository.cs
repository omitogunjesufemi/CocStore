using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);

        Task<Role?> GetRoleAsync(string roleName);
        Task<RefreshToken?> GetRefreshToken(string refreshToken);
        Task<RefreshToken?> GetRefreshTokenWithJWTID(string jwtId);

        Task<User> GetUserByEmail(string email);
    }
}
