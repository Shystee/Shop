using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shop.Contracts.ContractBindings;

namespace Shop.Contracts.V1.Requests.Queries
{
    public class SortingQuery
    {
        public SortingQuery()
        {
            Sortings = new List<Sorting>();
        }

        [FromQuery(Name = "sort")]
        [BindProperty(BinderType = typeof(SortingModelBinder))]
        public IEnumerable<Sorting> Sortings { get; set; }
    }

    public class Sorting
    {
        public string Name { get; set; }

        public SortingDirections Direction { get; set; }
    }
}