using System.Collections.Generic;
using Shop.Api.Domain;
using Shop.Api.Infrastructure.Core.Queries;
using Shop.Contracts.V1.Responses;

namespace Shop.Api.Features.Queries
{
    public class GetProductsQuery : IQuery<List<ProductResponse>>
    {
        public GetAllProductsFilter Filter { get; set; }

        public PaginationFilter Pagination { get; set; }

        public SortingFilter Sorting { get; set; }
    }
}