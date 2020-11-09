namespace Shop.Contracts.V1.Requests.Queries
{
    public class SortingQuery
    {
        public SortingDirections Direction { get; set; }

        public string Name { get; set; }
    }
}