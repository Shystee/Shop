using System.Collections.Generic;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.Domain
{
    public class SortingFilter
    {
        public IEnumerable<Sorting> Sortings { get; set; }
    }
}