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
        private readonly IPaginationService paginationService;
        private readonly ISortingService sortingService;
        private readonly IUriService uriService;

        public ProductsController(
            IMediator mediator,
            IMapper mapper,
            IUriService uriService,
            IPaginationService paginationService,
            ISortingService sortingService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.uriService = uriService;
            this.paginationService = paginationService;
            this.sortingService = sortingService;
        }

        /// <summary>
        ///     Creates product in the system
        /// </summary>
        /// <response code="201">Returns product</response>
        /// <response code="400">Unable to create product due to validation error</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Products.Create)]
        public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
        {
            var result = await mediator.Send(mapper.Map<CreateProductCommand>(request)).ConfigureAwait(false);

            return Created(uriService.GetProductUri(result.Id), new Response<ProductResponse>(result));
        }

        /// <summary>
        ///     Creates product rating in the system
        /// </summary>
        /// <response code="201">Returns product rating</response>
        /// <response code="400">Unable to create product rating due to validation error</response>
        /// <response code="404">Unable to create product rating due to product not existing</response>
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

        /// <summary>
        ///     Deletes product in the system
        /// </summary>
        /// <response code="204">No content</response>
        /// <response code="400">Unable to delete product due to validation error</response>
        /// <response code="404">Unable to delete product due to it not existing</response>
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

        /// <summary>
        ///     Retrieve product in the system
        /// </summary>
        /// <response code="200">Returns product</response>
        /// <response code="400">Unable to retrieve product due to validation error</response>
        /// <response code="404">Unable to retrieve product due to it not existing</response>
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

        /// <summary>
        ///     Retrieve products in the system
        /// </summary>
        /// <response code="200">Returns products</response>
        [HttpGet(ApiRoutes.Products.GetAll)]
        public async Task<IActionResult> GetAll(
            [FromQuery]GetAllProductsQuery query,
            [FromQuery]PaginationQuery paginationQuery,
            [FromQuery]SortQuery sortQuery)
        {
            var sortingFilter = sortingService.GetProductSortingFilters(sortQuery);
            var filter = mapper.Map<GetAllProductsFilter>(query);
            var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);
            var productQuery = new GetProductsQuery
            {
                Filter = filter,
                Pagination = paginationFilter,
                Sorting = sortingFilter
            };

            var result = await mediator.Send(productQuery).ConfigureAwait(false);

            return Ok(paginationService.CreateProductPaginatedResponse(paginationFilter,
                filter,
                sortingFilter,
                result));
        }

        /// <summary>
        ///     Retrieve product ratings in the system
        /// </summary>
        /// <response code="200">Returns product ratings</response>
        /// <response code="404">Unable to retrieve product ratings due to product not existing</response>
        [HttpGet(ApiRoutes.ProductRatings.GetAll)]
        public async Task<IActionResult> GetProductRatings(
            [FromRoute]int productId,
            [FromQuery]GetAllRatingsQuery query,
            [FromQuery]PaginationQuery paginationQuery,
            [FromQuery]SortQuery sortQuery)
        {
            var filter = mapper.Map<GetAllRatingsFilter>(query);
            var sortingFilter = sortingService.GetRatingSortingFilters(sortQuery);
            var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);
            var productQuery = new GetRatingsQuery
            {
                ProductId = productId,
                Filter = filter,
                Pagination = paginationFilter,
                Sorting = sortingFilter
            };

            var result = await mediator.Send(productQuery).ConfigureAwait(false);

            return Ok(paginationService.CreateProductRatingsPaginatedResponse(productId,
                paginationFilter,
                filter,
                sortingFilter,
                result));
        }

        /// <summary>
        ///     Updates product in the system
        /// </summary>
        /// <response code="200">Returns updated product</response>
        /// <response code="400">Unable to update product due validation errors</response>
        /// <response code="404">Unable to update product due to it not existing</response>
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