using System.Collections.Generic;
using Shop.Api.Domain;
using Shop.Api.Infrastructure.Core.Queries;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Queries
{
    public class GetRatingsQuery : IQuery<List<RatingResponse>>
    {
        public GetAllRatingsFilter Filter { get; set; }

        public PaginationFilter Pagination { get; set; }

        public int ProductId { get; set; }

        public SortingFilter Sorting { get; set; }
    }
}