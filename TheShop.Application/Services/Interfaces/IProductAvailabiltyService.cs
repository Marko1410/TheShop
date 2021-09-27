using System.Collections.Generic;
using TheShop.Domain.Models;

namespace TheShop.Application.Services.Interfaces
{
    public interface IProductAvailabiltyService
    {
        IEnumerable<ProductAvailability> GetProductAvailabilities(IEnumerable<Supplier> suppliers, Product product, decimal priceLimit);
    }
}
