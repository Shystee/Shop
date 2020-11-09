using MediatR;

namespace Shop.Api.Infrastructure.Core.Commands
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
            where TRequest : ICommand<TResponse>
    {
    }

    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest> where TRequest : ICommand
    {
    }
}