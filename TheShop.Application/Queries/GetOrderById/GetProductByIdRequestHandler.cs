using System;
using TheShop.Application.Models.DTOs;
using TheShop.Application.Models.Reponses;
using TheShop.Application.Repositories;

namespace TheShop.Application.Queries.GetOrderById
{
    public class GetProductByIdRequestHandler
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdRequestHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public Result<ProductDTO> Handle(GetProductByIdRequest request)
        {
            var product = productRepository.GetProductById(request.ProductId);
            if (product == null)
                return Result<ProductDTO>.Error("Product does not exist!");

            var productDTO = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name
            };

            return Result<ProductDTO>.Success(productDTO);
        }
    }
}
