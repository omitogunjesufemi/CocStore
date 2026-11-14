using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);

        Task<Role> GetRoleAsync(string roleName);

        Task<User> GetUserByEmail(string email);
    }
}
