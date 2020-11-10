using Microsoft.AspNetCore.Mvc;

namespace Shop.Contracts.V1.Requests.Queries
{
    public class GetAllProductsQuery
    {
        [FromQuery(Name = "description")]
        public string Description { get; set; }

        [FromQuery(Name = "name")]
        public string Name { get; set; }

        [FromQuery(Name = "priceFrom")]
        public decimal? PriceFrom { get; set; }

        [FromQuery(Name = "priceTo")]
        public decimal? PriceTo { get; set; }

        [FromQuery(Name = "ratingFrom")]
        public double? RatingFrom { get; set; }
    }
}