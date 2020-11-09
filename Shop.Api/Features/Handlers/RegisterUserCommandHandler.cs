using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Services;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class RegisterUserCommandHandler : ICommandHandler<RegistrationUserCommand, AuthSuccessResponse>
    {
        private readonly IIdentityService identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<AuthSuccessResponse> Handle(
            RegistrationUserCommand request,
            CancellationToken cancellationToken)
        {
            var authResponse = await identityService
                                     .RegisterAsync(request.Email, request.Password)
                                     .ConfigureAwait(false);

            if (!authResponse.Success)
            {
                throw new ValidationException(
                    authResponse.Errors.Select(x => new ValidationFailure(string.Empty, x)));
            }

            return new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            };
        }
    }
}