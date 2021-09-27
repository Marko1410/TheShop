using TheShop.Domain.Models;

namespace TheShop.Application.Repositories
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        void AddProduct(Product product);
    }
}
