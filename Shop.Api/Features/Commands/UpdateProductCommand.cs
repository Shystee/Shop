using Shop.Api.Infrastructure.Core.Commands;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Commands
{
    public class UpdateProductCommand : ICommand<ProductResponse>
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}