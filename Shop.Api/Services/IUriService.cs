using System;
using Shop.Api.Domain;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.Services
{
    public interface IUriService
    {
        Uri GetAllProductsUri(PaginationQuery pagination, GetAllProductsFilter filter, SortingFilter sorting);

        Uri GetProductRatingUri(
            int productId,
            PaginationQuery pagination,
            GetAllRatingsFilter filter,
            SortingFilter sorting);

        Uri GetProductUri(int productId);

        Uri GetRatingUri(int ratingId);
    }
}