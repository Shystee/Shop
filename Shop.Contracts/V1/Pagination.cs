namespace Shop.Contracts.V1
{
    public class Pagination
    {
        public string NextPage { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public string PreviousPage { get; set; }
    }
}