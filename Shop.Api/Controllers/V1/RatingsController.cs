using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Contracts;
using Shop.Contracts.V1;
using Shop.Contracts.V1.Requests;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.Controllers.V1
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class RatingsController : ControllerBase
    {
        private readonly IMediator mediator;

        private readonly IMapper mapper;

        public RatingsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet(ApiRoutes.Ratings.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRatingsQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            return Ok("Get all");
        }

        [HttpPut(ApiRoutes.Ratings.Update)]
        public async Task<IActionResult> Update([FromRoute] int ratingId, [FromBody] UpdateRatingRequest request)
        {
            return Ok("Update");
        }

        [HttpDelete(ApiRoutes.Ratings.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int ratingId)
        {
            return Ok("Delete");
        }

        [HttpGet(ApiRoutes.Ratings.Get)]
        public async Task<IActionResult> Get([FromRoute] int ratingId)
        {
            return Ok("Get");
        }

        [HttpPost(ApiRoutes.Ratings.Create)]
        public async Task<IActionResult> Create([FromBody] CreateRatingRequest request)
        {
            return Ok("Create");
        }
    }
}
