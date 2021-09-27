namespace TheShop.Domain.Models
{
    public class Supplier
    {
        public Supplier(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
