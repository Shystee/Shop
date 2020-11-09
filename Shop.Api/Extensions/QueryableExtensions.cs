using System.Linq;
using Shop.Api.Domain;

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
    }
}