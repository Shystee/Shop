using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Features.Commands;
using Shop.Contracts;
using Shop.Contracts.V1.Requests;

namespace Shop.Api.Controllers.V1
{
    [Produces("application/json")]
    public class IdentityController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public IdentityController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
        {
            var result = await mediator.Send(mapper.Map<LoginUserCommand>(request)).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest request)
        {
            var result = await mediator.Send(mapper.Map<RefreshTokenCommand>(request)).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {
            var result = await mediator.Send(mapper.Map<RegistrationUserCommand>(request))
                                       .ConfigureAwait(false);

            return Ok(result);
        }
    }
}