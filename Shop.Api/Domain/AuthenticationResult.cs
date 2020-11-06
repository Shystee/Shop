using System.Collections.Generic;

namespace Shop.Api.Domain
{
    public class AuthenticationResult
    {
        public IEnumerable<string> Errors { get; set; }

        public string RefreshToken { get; set; }

        public bool Success { get; set; }

        public string Token { get; set; }
    }
}