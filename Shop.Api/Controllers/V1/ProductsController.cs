using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Domain;
using Shop.Api.Extensions;
using Shop.Api.Features.Commands;
using Shop.Api.Features.Queries;
using Shop.Api.Services;
using Shop.Contracts;
using Shop.Contracts.V1;
using Shop.Contracts.V1.Requests;
using Shop.Contracts.V1.Requests.Queries;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Controllers.V1
{
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPost(ApiRoutes.Products.Create)]
        public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
        {
            var result = await mediator.Send(mapper.Map<CreateProductCommand>(request)).ConfigureAwait(false);

            return Created(uriService.GetProductUri(result.Id), new Response<ProductResponse>(result));
        }

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
            [FromQuery]SortingQuery sortingQuery)
        {
            var productQuery = new GetProductsQuery
            {
                Filter = mapper.Map<GetAllProductsFilter>(query),
                Pagination = mapper.Map<PaginationFilter>(paginationQuery),
                Sorting = mapper.Map<SortingFilter>(sortingQuery)
            };

            var result = await mediator.Send(productQuery).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<IActionResult> Update(
            [FromRoute]int productId,
            [FromBody]UpdateProductRequest request)
        {
            var result = await mediator.Send(mapper.MapWithId<UpdateProductCommand>(request, productId))
                                       .ConfigureAwait(false);

            return Ok(new Response<ProductResponse>(result));
        }
    }
}