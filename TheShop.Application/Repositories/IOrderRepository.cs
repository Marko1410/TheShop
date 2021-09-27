using TheShop.Domain.Models;

namespace TheShop.Application.Repositories
{
    public interface IOrderRepository
    {
        void SaveOrder(Order order);
    }
}
