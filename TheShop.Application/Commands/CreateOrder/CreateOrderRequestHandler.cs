using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TheShop.Application.Infrastructure.Exceptions;
using TheShop.Application.Models.DTOs;
using TheShop.Application.Models.Reponses;
using TheShop.Application.Repositories;
using TheShop.Application.Services.Interfaces;
using TheShop.Domain.Models;

namespace TheShop.Application.Commands.CreateOrder
{
    public class CreateOrderRequestHandler
    {
        private readonly IProductRepository productRepository;
        private readonly ISupplierRepository supplierRepository;
        private readonly IProductAvailabiltyService productAvailabilityService;
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public CreateOrderRequestHandler(IProductRepository productRepository,
                                         ISupplierRepository supplierRepository,
                                         IProductAvailabiltyService productAvailabilityService,
                                         IOrderRepository orderRepository,
                                         ICustomerRepository customerRepository,
                                         ILogger logger,
                                         IMapper mapper)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            this.productAvailabilityService = productAvailabilityService ?? throw new ArgumentNullException(nameof(productAvailabilityService));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Result<OrderDTO> Handle(CreateOrderRequest request)
        { 
            try
            {
                var product = productRepository.GetProductById(request.ProductId);
                if (product == null)
                    return Result<OrderDTO>.Error("Product does not exist!");

                var customer = customerRepository.GetCustomerById(request.CustomerId);
                if (customer == null)
                    return Result<OrderDTO>.Error("Invalid customer id, customer does not exist!");

                var suppliers = supplierRepository.GetAllSuppliers();
                if (suppliers == null)
                    return Result<OrderDTO>.Error("Product is unavailable!");

                var productAvailabilities = productAvailabilityService.GetProductAvailabilities(suppliers, product, request.PriceLimit);
                if (!productAvailabilities.Any())
                    return Result<OrderDTO>.Error("Product out of stock!");

                var bestOffer = GetBestOffer(productAvailabilities);
                var order = CreateOrder(bestOffer, customer, product);

                logger.Debug($"Trying to sell product with id: {product.Id}");
                orderRepository.SaveOrder(order);
                logger.Info($"Product with id: {product.Id} is sold.");

                var orderDTO = mapper.Map<OrderDTO>(order);
                return Result<OrderDTO>.Success(orderDTO);
            }
            catch (PriceLimitException ex)
            {
                return Result<OrderDTO>.Error("Product is unavailable. Try higher price limit.");
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to create order!");
                return Result<OrderDTO>.Error(ex.Message);
            }
        }

        private ProductAvailability GetBestOffer(IEnumerable<ProductAvailability> productAvailabilities)
        {
            return productAvailabilities.OrderBy(x => x.Price).FirstOrDefault();
        }

        private Order CreateOrder(ProductAvailability productAvailability, Customer customer, Product product)
        {
            var order = new Order()
            {
                Supplier = productAvailability.Supplier,
                OrderAmount = productAvailability.Price,
                Product = product,
                Customer = customer,
                CreatedOnUtc = DateTime.UtcNow
            };
            return order;
        }
    }
}
