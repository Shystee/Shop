using System;
using Shop.Contracts;
using Shop.Contracts.V1;

namespace Shop.Api.Services
{
    public class UriService : IUriService
    {
        private readonly string baseUri;

        public UriService(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public Uri GetProductUri(int productId)
        {
            return new Uri(baseUri + ApiRoutes.Products.Get.Replace("{productId}", productId.ToString()));
        }
    }
}