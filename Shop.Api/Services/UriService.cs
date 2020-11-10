using System;
using Shop.Api.Domain;
using Shop.Api.Extensions;
using Shop.Contracts;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.Services
{
    public class UriService : IUriService
    {
        private readonly string baseUri;

        public UriService(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public Uri GetAllProductsUri(
            PaginationQuery pagination,
            GetAllProductsFilter filter,
            SortingFilter sorting)
        {
            var uri = new Uri(baseUri + ApiRoutes.Products.GetAll);

            var modifiedUri = uri.AbsoluteUri.GeneratePaginationQuery(pagination)
                                 .GenerateProductFilterQuery(filter)
                                 .GenerateSortingQuery(sorting);

            return new Uri(modifiedUri);
        }

        public Uri GetProductRatingUri(
            int productId,
            PaginationQuery pagination,
            GetAllRatingsFilter filter,
            SortingFilter sorting)
        {
            var uri = new Uri(baseUri
                            + ApiRoutes.ProductRatings.GetAll.Replace("{productId}", productId.ToString()));

            var modifiedUri = uri.AbsoluteUri.GeneratePaginationQuery(pagination)
                                 .GenerateRatingFilterQuery(filter)
                                 .GenerateSortingQuery(sorting);

            return new Uri(modifiedUri);
        }

        public Uri GetProductUri(int productId)
        {
            return new Uri(baseUri + ApiRoutes.Products.Get.Replace("{productId}", productId.ToString()));
        }

        public Uri GetRatingUri(int ratingId)
        {
            return new Uri(baseUri + ApiRoutes.Ratings.Get.Replace("{ratingId}", ratingId.ToString()));
        }
    }
}