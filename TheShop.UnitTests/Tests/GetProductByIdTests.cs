using Moq;
using TheShop.Application.Queries.GetOrderById;
using TheShop.Application.Repositories;
using TheShop.Domain.Models;
using Xunit;

namespace TheShop.UnitTests.Tests
{
    public class GetProductByIdTests
    {
        [Fact]
        public void Handle_ShouldDataBeNullIfProductNotExsits()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns<Product>(null);

            var productId = 1;

            var request = new GetProductByIdRequest()
            {
                ProductId = productId
            };

            var handler = new GetProductByIdRequestHandler(mockProductRepository.Object);
            var result = handler.Handle(request);

            Assert.Null(result.Data);
        }

        [Fact]
        public void Handle_ShouldBeSuccessfulIfProductExsist()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductById(1))
                .Returns(new Product(1, "Product1"));

            var request = new GetProductByIdRequest()
            {
                ProductId = 1
            };

            var handler = new GetProductByIdRequestHandler(mockProductRepository.Object);
            var result = handler.Handle(request);

            Assert.True(result.IsSuccessful);
        }
    }
}
