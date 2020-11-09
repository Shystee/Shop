namespace Shop.Contracts.V1.Requests
{
    public class UpdateRatingRequest
    {
        public string Comment { get; set; }

        public double Value { get; set; }
    }
}