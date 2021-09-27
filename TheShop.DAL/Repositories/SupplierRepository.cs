using System.Collections.Generic;
using TheShop.Application.Repositories;
using TheShop.Domain.Models;

namespace TheShop.DAL.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private IList<Supplier> suppliers;

        public SupplierRepository()
        {
            suppliers = new List<Supplier>();
            suppliers.Add(new Supplier(1, "supplier1"));
            suppliers.Add(new Supplier(2, "supplier2"));
            suppliers.Add(new Supplier(3, "supplier3"));
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return suppliers;
        }
    }
}
