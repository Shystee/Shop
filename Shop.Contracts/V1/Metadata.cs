using System.Collections.Generic;
using Shop.Contracts.V1.Requests;

namespace Shop.Contracts.V1
{
    public class Metadata
    {
        public Pagination Pagination { get; set; }

        public List<Sorting> SortedBy { get; set; }
    }
}