using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Repositories;
using Shop.Contracts.V1.Responses;
using Shop.DataAccess.Entities;

namespace Shop.Api.Features.Handlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductResponse> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            await productRepository.AddAsync(product).ConfigureAwait(false);
            await productRepository.SaveAsync().ConfigureAwait(false);

            return mapper.Map<ProductResponse>(product);
        }
    }
}