using System;
using System.Linq;
using Shop.Api.Domain;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.Services
{
    /// <summary>
    /// Filters columns that can be sorted
    /// </summary>
    public interface ISortingService
    {
        public SortingFilter GetRatingSortingFilters(SortQuery query);

        public SortingFilter GetProductSortingFilters(SortQuery query);
    }

    public class SortingService : ISortingService
    {
        private static readonly string[] ProductSortings = { "description", "name", "price", "averageRating" };
        private static readonly string[] RatingSortings = { "comment", "value" };

        public SortingFilter GetRatingSortingFilters(SortQuery query)
        {
            return new SortingFilter
            {
                Sortings = query.Sortings.Where(x => RatingSortings.Contains(x.Name))
            };
        }

        public SortingFilter GetProductSortingFilters(SortQuery query)
        {
            return new SortingFilter
            {
                Sortings = query.Sortings.Where(x => ProductSortings.Contains(x.Name))
            };
        }
    }
}