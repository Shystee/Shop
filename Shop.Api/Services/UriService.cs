using System;
using Microsoft.AspNetCore.WebUtilities;
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

        public Uri GetAllProductsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(baseUri + ApiRoutes.Products.GetAll);

            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(uri.AbsoluteUri,
                "pageNumber",
                pagination.PageNumber.ToString());
            modifiedUri =
                    QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

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