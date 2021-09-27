using TheShop.Domain.Models;

namespace TheShop.Application.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int id);
    }
}
