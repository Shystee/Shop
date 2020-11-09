﻿namespace Shop.Api.Domain
{
    public class GetAllProductsFilter
    {
        public string Name { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public string Description { get; set; }

        public double? RatingFrom { get; set; }
    }
}