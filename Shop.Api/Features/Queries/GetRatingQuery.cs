using Shop.Api.Infrastructure.Core.Queries;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Queries
{
    public class GetRatingQuery : IQuery<RatingResponse>
    {
        public int RatingId { get; set; }
    }
}