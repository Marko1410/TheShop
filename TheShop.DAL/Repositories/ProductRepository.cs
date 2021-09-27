using System.Collections.Generic;
using System.Linq;
using TheShop.Application.Repositories;
using TheShop.Domain.Models;

namespace TheShop.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IList<Product> products;

        public ProductRepository()
        {
            products = new List<Product>();
            products.Add(new Product(1, "Product1"));
            products.Add(new Product(2, "Product2"));
            products.Add(new Product(3, "Product3"));
            products.Add(new Product(4, "Product4"));
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public Product GetProductById(int id)
        {
            return products.Where(x => x.Id == id)
                    .FirstOrDefault();
        }
    }
}
