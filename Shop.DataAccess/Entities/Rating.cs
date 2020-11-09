using Microsoft.AspNetCore.Identity;

namespace Shop.DataAccess.Entities
{
    public class Rating
    {
        public string Comment { get; set; }

        public int Id { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public IdentityUser User { get; set; }

        public string UserId { get; set; }

        public double Value { get; set; }
    }
}