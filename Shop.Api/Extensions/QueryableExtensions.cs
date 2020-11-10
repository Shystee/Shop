using System;
using System.Linq;
using System.Linq.Expressions;
using Shop.Api.Domain;
using Shop.Contracts.V1;

namespace Shop.Api.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(
            this IQueryable<T> queryable,
            PaginationFilter pagination)
        {
            if (pagination == null) return queryable;

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            return queryable.Skip(skip).Take(pagination.PageSize);
        }

        public static IOrderedQueryable<T> OrderBy<T, TKey>(
            this IQueryable<T> queryable,
            Expression<Func<T, TKey>> selector,
            SortingDirections directions)
        {
            return directions switch
            {
                SortingDirections.Ascending => queryable.OrderBy(selector),
                SortingDirections.Descending => queryable.OrderByDescending(selector),
                _ => throw new ArgumentOutOfRangeException(nameof(directions), directions, null)
            };
        }
    }
}