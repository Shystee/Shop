using System.Threading.Tasks;
using Shop.Api.Domain;

namespace Shop.Api.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}