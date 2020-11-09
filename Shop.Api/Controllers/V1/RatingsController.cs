using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Extensions;
using Shop.Api.Features.Commands;
using Shop.Api.Features.Queries;
using Shop.Contracts;
using Shop.Contracts.V1.Requests;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class RatingsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public RatingsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpDelete(ApiRoutes.Ratings.Delete)]
        public async Task<IActionResult> Delete([FromRoute]int ratingId)
        {
            var command = new DeleteRatingCommand
            {
                RatingId = ratingId,
                UserId = HttpContext.GetUserId()
            };

            await mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet(ApiRoutes.Ratings.Get)]
        public async Task<IActionResult> Get([FromRoute]int ratingId)
        {
            var query = new GetRatingQuery
            {
                RatingId = ratingId
            };

            var result = await mediator.Send(query).ConfigureAwait(false);

            return Ok(new Response<RatingResponse>(result));
        }

        [HttpPut(ApiRoutes.Ratings.Update)]
        public async Task<IActionResult> Update(
            [FromRoute]int ratingId,
            [FromBody]UpdateRatingRequest request)
        {
            var command = new UpdateRatingCommand
            {
                Id = ratingId,
                UserId = HttpContext.GetUserId()
            };
            mapper.Map(request, command);
            var result = await mediator.Send(command).ConfigureAwait(false);

            return Ok(new Response<RatingResponse>(result));
        }
    }
}