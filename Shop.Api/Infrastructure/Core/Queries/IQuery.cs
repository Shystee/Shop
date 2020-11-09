using MediatR;

namespace Shop.Api.Infrastructure.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}