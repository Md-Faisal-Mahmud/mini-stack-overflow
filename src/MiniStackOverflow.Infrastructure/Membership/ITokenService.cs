using System.Security.Claims;

namespace MiniStackOverflow.Infrastructure.Membership
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims, string key, string issuer, string audience);
    }
}