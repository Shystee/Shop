using MediatR;

namespace Shop.Api.Infrastructure.Core.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : IRequest
    {
    }
}