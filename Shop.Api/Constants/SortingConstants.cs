using System.Collections.Generic;
using Shop.Contracts.V1;

namespace Shop.Api.Constants
{
    public static class SortingConstants
    {
        public static readonly Dictionary<SortingDirections, string> DirectionsDictionary =
                new Dictionary<SortingDirections, string>
                {
                    { SortingDirections.Ascending, "ASC" },
                    { SortingDirections.Descending, "DESC" }
                };
    }
}