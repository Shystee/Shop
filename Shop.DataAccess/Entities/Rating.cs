using System;
using Microsoft.AspNetCore.Identity;

namespace Shop.DataAccess.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public double Value { get; set; }
    }
}