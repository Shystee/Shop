using System;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.Services
{
    public interface IUriService
    {
        Uri GetAllProductsUri(PaginationQuery pagination = null);

        Uri GetProductUri(int productId);

        Uri GetRatingUri(int ratingId);

        Uri GetProductRatingUri(int productId, PaginationQuery pagination = null);
    }
}