namespace Shop.Contracts.V1.Requests
{
    public class CreateProductRequest
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}