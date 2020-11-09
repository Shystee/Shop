namespace Shop.Contracts.V1.Requests
{
    public class UpdateProductRequest
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}