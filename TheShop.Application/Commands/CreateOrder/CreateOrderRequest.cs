namespace TheShop.Application.Commands.CreateOrder
{
    public class CreateOrderRequest 
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal PriceLimit { get; set; }
    }
}
