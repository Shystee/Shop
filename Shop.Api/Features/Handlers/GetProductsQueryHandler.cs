using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Api.Features.Queries;
using Shop.Api.Infrastructure.Core.Queries;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, List<ProductResponse>>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<List<ProductResponse>> Handle(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = await productRepository
                                 .GetAllAsync(request.Filter, request.Pagination, request.Sorting)
                                 .ConfigureAwait(false);

            return mapper.Map<List<ProductResponse>>(products);
        }
    }
}