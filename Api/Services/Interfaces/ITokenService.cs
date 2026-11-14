using Api.Models;

namespace Api.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IList<string> roles, out string jwtId);
        RefreshToken GenerateRefreshToken(string jwtId, Guid userId);
    }
}
