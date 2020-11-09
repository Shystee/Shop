using System.Collections.Generic;

namespace Shop.Contracts.V1.Responses
{
    public class ProductResponse
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<RatingResponse> Ratings { get; set; }
    }
}