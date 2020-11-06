namespace Shop.DataAccess.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public double Value { get; set; }
    }
}