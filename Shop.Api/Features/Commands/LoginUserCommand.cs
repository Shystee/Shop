using Shop.Api.Infrastructure.Core.Commands;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Commands
{
    public class LoginUserCommand : ICommand<AuthSuccessResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}