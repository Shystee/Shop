using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Shop.Api.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User == null
                           ? string.Empty
                           : httpContext.User.Claims.SingleOrDefault(x => x.Type == "id")?.Value;
        }
    }
}