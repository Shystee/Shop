using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Shop.Api.Constants;
using Shop.Api.Domain;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.Extensions
{
    public static class QueryExtensions
    {
        public static string GeneratePaginationQuery(this string uri, PaginationQuery pagination)
        {
            return uri.AddQuery("pageNumber", pagination.PageNumber)
                      .AddQuery("pageSize", pagination.PageSize);
        }

        public static string GeneratePaginationQuery(this string uri, PaginationFilter pagination)
        {
            return uri.AddQuery("pageNumber", pagination.PageNumber)
                      .AddQuery("pageSize", pagination.PageSize);
        }

        public static string GenerateProductFilterQuery(this string uri, GetAllProductsFilter filter)
        {
            return uri.AddQuery(nameof(filter.Name), filter.Name)
                      .AddQuery(nameof(filter.Description), filter.Description)
                      .AddQuery(nameof(filter.PriceFrom), filter.PriceFrom)
                      .AddQuery(nameof(filter.PriceTo), filter.PriceTo)
                      .AddQuery(nameof(filter.RatingFrom), filter.RatingFrom);
        }

        public static string GenerateRatingFilterQuery(this string uri, GetAllRatingsFilter filter)
        {
            return uri.AddQuery(nameof(filter.Comment), filter.Comment)
                      .AddQuery(nameof(filter.RatingFrom), filter.RatingFrom)
                      .AddQuery(nameof(filter.RatingTo), filter.RatingTo);
        }

        public static string GenerateSortingQuery(this string uri, SortingFilter sorting)
        {
            return uri.AddQuery("sort", SortingBuilder(sorting));
        }

        private static string AddQuery(this string uri, string name, object value)
        {
            if (value == null) return uri;

            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue)) return uri;

            return QueryHelpers.AddQueryString(uri, name.ToLower(), stringValue);
        }

        private static string SortingBuilder(SortingFilter sorting)
        {
            var sortBuilder = new StringBuilder();

            foreach (var sort in sorting.Sortings)
            {
                sortBuilder.Append($"{sort.Name}+{SortingConstants.DirectionsDictionary[sort.Direction]}");
            }

            return sortBuilder.ToString();
        }
    }
}