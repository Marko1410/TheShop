using System.Collections.Generic;
using TheShop.Application.Repositories;
using TheShop.Domain.Models;

namespace TheShop.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private IList<Order> orders;

        public OrderRepository()
        {
            orders = new List<Order>();
        }

        public void SaveOrder(Order order)
        {
            orders.Add(order);
        }
    }
}
