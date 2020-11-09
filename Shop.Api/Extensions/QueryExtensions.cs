using Microsoft.AspNetCore.WebUtilities;

namespace Shop.Api.Extensions
{
    public static class QueryExtensions
    {
        public static string AddQuery(this string uri, string name, object value)
        {
            return QueryHelpers.AddQueryString(uri, name, value.ToString());
        }
    }
}