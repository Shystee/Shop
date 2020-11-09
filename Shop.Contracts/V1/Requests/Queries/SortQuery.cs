using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shop.Contracts.ContractBindings;

namespace Shop.Contracts.V1.Requests.Queries
{
    public class SortQuery
    {
        public SortQuery()
        {
            Sortings = new List<SortingQuery>();
        }

        [FromQuery(Name = "sort")]
        [BindProperty(BinderType = typeof(SortingModelBinder))]
        public IEnumerable<SortingQuery> Sortings { get; set; }
    }
}