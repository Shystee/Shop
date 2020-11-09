namespace Shop.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = Root + "/" + Version;

        public const string Root = "api";

        public const string Version = "v1";

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Refresh = Base + "/identity/refresh";

            public const string Register = Base + "/identity/register";
        }

        public static class ProductRatings
        {
            public const string Create = Base + "/products/{productId}/ratings";

            public const string GetAll = Base + "/products/{productId}/ratings";
        }

        public static class Products
        {
            public const string Create = Base + "/products";

            public const string Delete = Base + "/products/{productId}";

            public const string Get = Base + "/products/{productId}";

            public const string GetAll = Base + "/products";

            public const string Update = Base + "/products/{productId}";
        }

        public static class Ratings
        {
            public const string Delete = Base + "/ratings/{ratingId}";

            public const string Get = Base + "/ratings/{ratingId}";

            public const string Update = Base + "/ratings/{ratingId}";
        }
    }
}