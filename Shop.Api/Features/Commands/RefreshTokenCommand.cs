using Shop.Api.Infrastructure.Core.Commands;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Commands
{
    public class RefreshTokenCommand : ICommand<AuthSuccessResponse>
    {
        public string RefreshToken { get; set; }

        public string Token { get; set; }
    }
}