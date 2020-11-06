namespace Shop.Contracts.V1.Responses
{
    public class AuthSuccessResponse
    {
        public string RefreshToken { get; set; }

        public string Token { get; set; }
    }
}