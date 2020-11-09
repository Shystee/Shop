using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Infrastructure.Exceptions;
using Shop.Api.Services;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AuthSuccessResponse>
    {
        private readonly IIdentityService identityService;

        public LoginUserCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<AuthSuccessResponse> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var authResponse =
                    await identityService.LoginAsync(request.Email, request.Password).ConfigureAwait(false);

            if (!authResponse.Success)
            {
                throw new ValidationException(authResponse.Errors.Select(x => new ErrorModel
                {
                    FieldName = string.Empty,
                    Message = x
                }));
            }

            return new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            };
        }
    }
}