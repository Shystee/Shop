using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Domain;
using Shop.Api.Extensions;
using Shop.Api.Features.Commands;
using Shop.Api.Features.Queries;
using Shop.Api.Helpers;
using Shop.Api.Services;
using Shop.Contracts;
using Shop.Contracts.V1.Requests;
using Shop.Contracts.V1.Requests.Queries;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Controllers.V1
{
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IUriService uriService;

        public ProductsController(IMediator mediator, IMapper mapper, IUriService uriService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.uriService = uriService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Products.Create)]
        public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
        {
            var result = await mediator.Send(mapper.Map<CreateProductCommand>(request)).ConfigureAwait(false);

            return Created(uriService.GetProductUri(result.Id), new Response<ProductResponse>(result));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.ProductRatings.Create)]
        public async Task<IActionResult> CreateProductRating(
            [FromRoute]int productId,
            [FromBody]CreateRatingRequest request)
        {
            var command = new CreateRatingCommand
            {
                ProductId = productId,
                UserId = HttpContext.GetUserId()
            };
            mapper.Map(request, command);
            var result = await mediator.Send(command).ConfigureAwait(false);

            return Created(uriService.GetRatingUri(result.Id), new Response<RatingResponse>(result));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(ApiRoutes.Products.Delete)]
        public async Task<IActionResult> Delete([FromRoute]int productId)
        {
            var command = new DeleteProductCommand
            {
                ProductId = productId
            };

            await mediator.Send(command).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet(ApiRoutes.Products.Get)]
        public async Task<IActionResult> Get([FromRoute]int productId)
        {
            var query = new GetProductQuery
            {
                ProductId = productId
            };

            var result = await mediator.Send(query).ConfigureAwait(false);

            return Ok(new Response<ProductResponse>(result));
        }

        [HttpGet(ApiRoutes.Products.GetAll)]
        public async Task<IActionResult> GetAll(
            [FromQuery]GetAllProductsQuery query,
            [FromQuery]PaginationQuery paginationQuery,
            [FromQuery]SortQuery sortQuery)
        {
            var sortingFilter = mapper.Map<SortingFilter>(sortQuery);
            var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);
            var productQuery = new GetProductsQuery
            {
                Filter = mapper.Map<GetAllProductsFilter>(query),
                Pagination = paginationFilter,
                Sorting = sortingFilter
            };

            var result = await mediator.Send(productQuery).ConfigureAwait(false);

            return Ok(PaginationHelpers.CreatePaginatedResponse(uriService,
                paginationFilter,
                sortingFilter,
                result));
        }

        [HttpGet(ApiRoutes.ProductRatings.GetAll)]
        public async Task<IActionResult> GetProductRatings(
            [FromRoute]int productId,
            [FromQuery]GetAllRatingsQuery query,
            [FromQuery]PaginationQuery paginationQuery,
            [FromQuery]SortQuery sortQuery)
        {
            var sortingFilter = mapper.Map<SortingFilter>(sortQuery);
            var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);
            var productQuery = new GetRatingsQuery
            {
                ProductId = productId,
                Filter = mapper.Map<GetAllRatingsFilter>(query),
                Pagination = paginationFilter,
                Sorting = sortingFilter
            };

            var result = await mediator.Send(productQuery).ConfigureAwait(false);

            return Ok(PaginationHelpers.CreatePaginatedResponse(uriService,
                paginationFilter,
                sortingFilter,
                result));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<IActionResult> Update(
            [FromRoute]int productId,
            [FromBody]UpdateProductRequest request)
        {
            var command = new UpdateProductCommand
            {
                Id = productId
            };
            mapper.Map(request, command);
            var result = await mediator.Send(command).ConfigureAwait(false);

            return Ok(new Response<ProductResponse>(result));
        }
    }
}