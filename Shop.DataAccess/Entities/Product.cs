using System.Collections.ObjectModel;

namespace Shop.DataAccess.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Collection<Rating> Ratings { get; set; } = new Collection<Rating>();
    }
}