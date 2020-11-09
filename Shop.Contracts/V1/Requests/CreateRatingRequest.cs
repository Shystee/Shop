namespace Shop.Contracts.V1.Requests
{
    public class CreateRatingRequest
    {
        public string Comment { get; set; }

        public double Value { get; set; }
    }
}