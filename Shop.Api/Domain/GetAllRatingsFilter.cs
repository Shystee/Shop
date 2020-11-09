namespace Shop.Api.Domain
{
    public class GetAllRatingsFilter
    {
        public string Comment { get; set; }

        public double? RatingFrom { get; set; }

        public double? RatingTo { get; set; }
    }
}