namespace TheShop.Domain.Models
{
    public class ProductAvailability
    {
        public Supplier Supplier { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
    }
}
