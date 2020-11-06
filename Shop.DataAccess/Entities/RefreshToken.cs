using System;
using Microsoft.AspNetCore.Identity;

namespace Shop.DataAccess.Entities
{
    public class RefreshToken
    {
        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Invalidated { get; set; }

        public string JwtId { get; set; }

        public string Token { get; set; }

        public bool Used { get; set; }

        public IdentityUser User { get; set; }

        public string UserId { get; set; }
    }
}