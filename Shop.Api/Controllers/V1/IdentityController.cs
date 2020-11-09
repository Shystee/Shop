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

        /// <summary>
        /// Logins user to the system
        /// </summary>
        /// <response code="200">Return user token</response>
        /// <response code="400">Unable to login user due to validation error</response>
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
        {
            var result = await mediator.Send(mapper.Map<LoginUserCommand>(request)).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Refreshes users token
        /// </summary>
        /// <response code="200">Return user token</response>
        /// <response code="400">Unable to refresh users token due to validation error</response>
        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest request)
        {
            var result = await mediator.Send(mapper.Map<RefreshTokenCommand>(request)).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Registers user to the system
        /// </summary>
        /// <response code="200">Return user token</response>
        /// <response code="400">Unable to register user due to validation error</response>
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {
            var result = await mediator.Send(mapper.Map<RegistrationUserCommand>(request))
                                       .ConfigureAwait(false);

            return Ok(result);
        }
    }
}