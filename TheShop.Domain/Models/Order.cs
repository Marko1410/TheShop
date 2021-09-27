using System;

namespace TheShop.Domain.Models
{
    public class Order
    {
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public decimal OrderAmount { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
