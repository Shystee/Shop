using Shop.Api.Infrastructure.Core.Queries;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Queries
{
    public class GetProductQuery : IQuery<ProductResponse>
    {
        public int ProductId { get; set; }
    }
}