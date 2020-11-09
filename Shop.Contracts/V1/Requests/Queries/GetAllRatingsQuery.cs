using Microsoft.AspNetCore.Mvc;

namespace Shop.Contracts.V1.Requests.Queries
{
    public class GetAllRatingsQuery
    {
        [FromQuery(Name = "comment")]
        public string Comment { get; set; }

        [FromQuery(Name = "ratingFrom")]
        public double? RatingFrom { get; set; }

        [FromQuery(Name = "ratingTo")]
        public double? RatingTo { get; set; }
    }
}