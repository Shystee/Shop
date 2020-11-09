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
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, AuthSuccessResponse>
    {
        private readonly IIdentityService identityService;

        public RefreshTokenCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<AuthSuccessResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var authResponse = await identityService
                                     .RefreshTokenAsync(request.Token, request.RefreshToken)
                                     .ConfigureAwait(false);

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