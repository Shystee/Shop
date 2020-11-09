namespace Shop.Contracts.V1.Requests
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}