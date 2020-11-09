using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Handlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductResponse> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (product is null)
            {
                throw new ArgumentNullException($"Could not find {nameof(product)} with id '{request.Id}'");
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            await productRepository.SaveAsync().ConfigureAwait(false);

            return mapper.Map<ProductResponse>(product);
        }
    }
}