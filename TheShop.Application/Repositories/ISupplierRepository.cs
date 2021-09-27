using System.Collections.Generic;
using TheShop.Domain.Models;

namespace TheShop.Application.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetAllSuppliers();
    }
}
