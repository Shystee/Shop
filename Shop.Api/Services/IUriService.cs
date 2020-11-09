using System;

namespace Shop.Api.Services
{
    public interface IUriService
    {
        Uri GetProductUri(int productId);
    }
}