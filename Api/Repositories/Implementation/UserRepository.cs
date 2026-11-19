using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApiDbContext _dbContext;

        public UserRepository(ApiDbContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Set<User>().AnyAsync(u => u.Email == email);
        }

        public async Task<Role?> GetRoleAsync(string roleName)
        {
            return await _dbContext.Set<Role>().FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<RefreshToken?> GetRefreshToken(string refreshToken)
        {
            return await _dbContext.Set<RefreshToken>().Include(rt => rt.User).ThenInclude(u => u.Roles).FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        }

        public async Task<RefreshToken?> GetRefreshTokenWithJWTID(string jwtId)
        {
            return await _dbContext.Set<RefreshToken>().FirstOrDefaultAsync(rt => rt.JwtId == jwtId);
        }


        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Set<User>().Include(ur => ur.Roles).FirstOrDefaultAsync(u => u.Email == email & u.IsActive);
        }
    }
}
