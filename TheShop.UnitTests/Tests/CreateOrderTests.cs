using AutoMapper;
using Moq;
using System.Collections.Generic;
using TheShop.Application.Commands.CreateOrder;
using TheShop.Application.Repositories;
using TheShop.Application.Services.Interfaces;
using TheShop.Domain.Models;
using Xunit;

namespace TheShop.UnitTests.Tests
{
    public class CreateOrderTests
    {
        private Mock<IProductRepository> mockProductRepository;
        private Mock<ICustomerRepository> mockCustomerRepository;
        private Mock<ISupplierRepository> mockSupplierRepository;
        private Mock<IOrderRepository> mockOrderRepository;
        private Mock<IProductAvailabiltyService> mockProductAvailabilityService;
        private Mock<ILogger> mockLogger;
        private Mock<IMapper> mockMapper;
        

        public CreateOrderTests()
        {
            mockProductRepository = new Mock<IProductRepository>();
            mockCustomerRepository = new Mock<ICustomerRepository>();
            mockOrderRepository = new Mock<IOrderRepository>();
            mockSupplierRepository = new Mock<ISupplierRepository>();
            mockProductAvailabilityService = new Mock<IProductAvailabiltyService>();
            mockLogger = new Mock<ILogger>();
            mockMapper = new Mock<IMapper>();
        }


        [Fact]
        public void Handle_ShouldBeUnsuccessfulIfProductDoesNotExists()
        {
            mockProductRepository.Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns<Product>(null);

            var request = new CreateOrderRequest()
            {
                CustomerId = 1,
                PriceLimit = 120,
                ProductId = 1
            };

            var handler = new CreateOrderRequestHandler(mockProductRepository.Object, mockSupplierRepository.Object,
                mockProductAvailabilityService.Object, mockOrderRepository.Object, mockCustomerRepository.Object, mockLogger.Object, mockMapper.Object);
            var result = handler.Handle(request);

            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void Handle_ShouldBeUnsuccessfulIfCustomerDoesNotExists()
        {
            mockProductRepository.Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(new Product(1, "Product1"));

            mockCustomerRepository.Setup(x => x.GetCustomerById(It.IsAny<int>()))
                .Returns<Customer>(null);

            var request = new CreateOrderRequest()
            {
                CustomerId = 1,
                PriceLimit = 120,
                ProductId = 1
            };

            var handler = new CreateOrderRequestHandler(mockProductRepository.Object, mockSupplierRepository.Object,
                mockProductAvailabilityService.Object, mockOrderRepository.Object, mockCustomerRepository.Object, mockLogger.Object, mockMapper.Object);
            var result = handler.Handle(request);

            Assert.False(result.IsSuccessful);
        }


        [Fact]
        public void Handle_ShouldBeUnsuccessfulIfSuppliersDoesNotExists()
        {
            mockProductRepository.Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(new Product(1, "Product1"));

            mockCustomerRepository.Setup(x => x.GetCustomerById(It.IsAny<int>()))
                .Returns(new Customer(1, "Customer1"));

            mockSupplierRepository.Setup(x => x.GetAllSuppliers())
                .Returns<IEnumerable<Supplier>>(null);

            var request = new CreateOrderRequest()
            {
                CustomerId = 1,
                PriceLimit = 120,
                ProductId = 1
            };

            var handler = new CreateOrderRequestHandler(mockProductRepository.Object, mockSupplierRepository.Object,
                mockProductAvailabilityService.Object, mockOrderRepository.Object, mockCustomerRepository.Object, mockLogger.Object, mockMapper.Object);
            var result = handler.Handle(request);

            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void Handle_ShouldBeUnsuccessfulIfProductAvailabilityIsNull()
        {
            var product = new Product(1, "Product1");

            mockProductRepository.Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(product);

            mockCustomerRepository.Setup(x => x.GetCustomerById(It.IsAny<int>()))
                .Returns(new Customer(1, "Customer1"));

            var suppliers = GetAllSuppliers();

            mockSupplierRepository.Setup(x => x.GetAllSuppliers())
                .Returns(suppliers);

            var priceLimit = 100;

            mockProductAvailabilityService.Setup(x => x.GetProductAvailabilities(suppliers, product, priceLimit))
                .Returns<ProductAvailability>(null);
                
            var request = new CreateOrderRequest()
            {
                CustomerId = 1,
                PriceLimit = priceLimit,
                ProductId = 1
            };

            var handler = new CreateOrderRequestHandler(mockProductRepository.Object, mockSupplierRepository.Object,
                mockProductAvailabilityService.Object, mockOrderRepository.Object, mockCustomerRepository.Object, mockLogger.Object, mockMapper.Object);

            var result = handler.Handle(request);

            Assert.False(result.IsSuccessful);
        }

        private IEnumerable<Supplier> GetAllSuppliers()
        {
            var suppliers = new List<Supplier>();

            suppliers.Add(new Supplier(1, "Supplier1"));
            suppliers.Add(new Supplier(2, "Supplier2"));
            suppliers.Add(new Supplier(3, "Supplier3"));

            return suppliers;
        }
    }
}
