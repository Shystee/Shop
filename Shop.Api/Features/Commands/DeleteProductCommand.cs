using Shop.Api.Infrastructure.Core.Commands;

namespace Shop.Api.Features.Commands
{
    public class DeleteProductCommand : ICommand
    {
        public int ProductId { get; set; }
    }
}