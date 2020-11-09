using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Api.Features.Queries;
using Shop.Api.Infrastructure.Core.Queries;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;
using Shop.DataAccess.Entities;

namespace Shop.Api.Features.Handlers
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductResponse>
    {
        private readonly IMapper mapper;
        private readonly IReadOnlyProductRepository productRepository;

        public GetProductQueryHandler(IReadOnlyProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductResponse> Handle(
            GetProductQuery request,
            CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId).ConfigureAwait(false);

            if (product is null)
            {
                throw new ArgumentNullException(
                    $"Could not find {nameof(product)} with id '{request.ProductId}'");
            }

            return mapper.Map<ProductResponse>(product);
        }
    }
}