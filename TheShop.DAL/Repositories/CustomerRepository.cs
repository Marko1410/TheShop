using System.Collections.Generic;
using System.Linq;
using TheShop.Application.Repositories;
using TheShop.Domain.Models;

namespace TheShop.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private IList<Customer> customers;

        public CustomerRepository()
        {
            customers = new List<Customer>();
            customers.Add(new Customer(1, "Customer1"));
            customers.Add(new Customer(2, "Customer2"));
            customers.Add(new Customer(3, "Customer3"));
            customers.Add(new Customer(4, "Customer4"));
        }

        public Customer GetCustomerById(int id)
        {
            return customers.Where(x => x.Id == id)
                            .FirstOrDefault();
        }
    }
}
