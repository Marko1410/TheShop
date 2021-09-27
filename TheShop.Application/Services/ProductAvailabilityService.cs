using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TheShop.Application.Infrastructure.Exceptions;
using TheShop.Application.Models.Reponses;
using TheShop.Application.Services.Interfaces;
using TheShop.Domain.Models;

namespace TheShop.Application.Services
{
    public class ProductAvailabilityService : IProductAvailabiltyService
    {
        private readonly IMapper mapper;

        public ProductAvailabilityService(IMapper mapper)
        {
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<ProductAvailability> GetProductAvailabilities(IEnumerable<Supplier> suppliers, Product product, decimal priceLimit)
        {
            var productAvailabilities = new List<ProductAvailability>();

            foreach(var supplier in suppliers)
            {
                var productAvailability = GetProductAvailability(supplier, product);
                if (productAvailability == null)
                    continue;

                productAvailabilities.Add(productAvailability);
            }

            if (productAvailabilities.Count > 0)
                return RemoveProductsWithPriceOverTheLimit(productAvailabilities, priceLimit);
            else
                return null;
        }

        private ProductAvailability GetProductAvailability(Supplier supplier, Product product)
        {
            var result = CallExternalService(supplier, product);

            if (!result.IsSuccessful)
                return null;

            var productAvailability = mapper.Map<ProductAvailability>(result.Data);

            if (!IsProductOnStock(productAvailability))
                return null;

            return productAvailability;
        }

        // simulating calling external api 
        private Result<ProductAvailabilityResponse> CallExternalService(Supplier supplier, Product product)
        {
            if(supplier.Id == 1)
            {
                var productAvailability = new ProductAvailabilityResponse()
                {
                    Price = 100,
                    Quantity = 143
                };
                return Result<ProductAvailabilityResponse>.Success(productAvailability);
            }

            if (supplier.Id == 2)
            {
                var productAvailability = new ProductAvailabilityResponse()
                {
                    Price = 80,
                    Quantity = 0
                };
                return Result<ProductAvailabilityResponse>.Success(productAvailability);
            }

            if (supplier.Id == 3)
            {
                var productAvailability = new ProductAvailabilityResponse()
                {
                    Price = 154,
                    Quantity = 999
                };
                return Result<ProductAvailabilityResponse>.Success(productAvailability);
            }

            return Result<ProductAvailabilityResponse>.Error("No supplier available");
        }

        private IEnumerable<ProductAvailability> RemoveProductsWithPriceOverTheLimit(IEnumerable<ProductAvailability> productAvailabilities, decimal priceLimit)
        {
            var inPrice = productAvailabilities.Where(x => x.Price <= priceLimit);
            if (!inPrice.Any())
                throw new PriceLimitException();

            return inPrice;    
        }

        private bool IsProductOnStock(ProductAvailability productAvailability)
        {
            return productAvailability.Quantity > 0;
        }
    }
}
