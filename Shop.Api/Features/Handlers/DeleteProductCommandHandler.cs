using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Api.Features.Commands;
using Shop.Api.Infrastructure.Core.Commands;
using Shop.Api.Repositories;

namespace Shop.Api.Features.Handlers
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId).ConfigureAwait(false);

            if (product is null)
            {
                throw new ArgumentNullException(
                    $"Could not find {nameof(product)} with id '{request.ProductId}'");
            }

            productRepository.Remove(product);
            await productRepository.SaveAsync().ConfigureAwait(false);

            return Unit.Value;
        }
    }
}