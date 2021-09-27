using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TheShop.Application.Commands.CreateOrder;
using TheShop.Application.Queries.GetOrderById;
using TheShop.Application.Repositories;
using TheShop.Application.Services;
using TheShop.Application.Services.Interfaces;
using TheShop.DAL.Repositories;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
            var services = new ServiceCollection();
            var config = ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var createOrderRequestHandler = serviceProvider.GetService<CreateOrderRequestHandler>();

            var createOrderRequest1 = new CreateOrderRequest()
            {
                CustomerId = 1,
                PriceLimit = 10,
                ProductId = 1
            };

            var result1 = createOrderRequestHandler.Handle(createOrderRequest1);
            if (result1.IsSuccessful)
            {
                Console.WriteLine("Succesfully placed order!");
                Console.WriteLine($"Order details: \nProduct name: {result1.Data.ProductName} \nPrice:{result1.Data.ProductPrice}");
            }
            else
            {
                Console.WriteLine($"\n{result1.ErrorMessage}");
            }

            var createOrderRequest2 = new CreateOrderRequest()
            {
                CustomerId = 1,
                PriceLimit = 120,
                ProductId = 1
            };

            var result2 = createOrderRequestHandler.Handle(createOrderRequest2);
            if (result2.IsSuccessful)
            {
                Console.WriteLine("Succesfully placed order!\n");
                Console.WriteLine($"Order details: \nProduct name: {result2.Data.ProductName} \nPrice:{result2.Data.ProductPrice}");
            }
            else
            {
                Console.WriteLine($"\n{result2.ErrorMessage}");
            }

            var getProductByIdRequest2 = new GetProductByIdRequest()
            {
                ProductId = 153
            };

            var getProductByIdRequestHandler = serviceProvider.GetService<GetProductByIdRequestHandler>();

            var result3 = getProductByIdRequestHandler.Handle(getProductByIdRequest2);
            if (result3.IsSuccessful)
            {
                Console.WriteLine($"\nProduct found: \nId: {result3.Data.Id} \nName: {result3.Data.Name}");
            }
            else
            {
                Console.WriteLine($"\n{result3.ErrorMessage}");
            }

            var getProductByIdRequest = new GetProductByIdRequest()
            {
                ProductId = 1
            };

            var result4 = getProductByIdRequestHandler.Handle(getProductByIdRequest);
            if (result4.IsSuccessful)
            {
                Console.WriteLine($"\nProduct found: \nId: {result4.Data.Id} \nName: {result4.Data.Name}");
            }
            else
            {
                Console.WriteLine($"\n{result4.ErrorMessage}");
            }
        }

		private static IConfiguration ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .Build();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IProductAvailabiltyService, ProductAvailabilityService>();
            services.AddTransient<ILogger, Logger>();

            services.AddTransient<GetProductByIdRequestHandler>();
            services.AddTransient<CreateOrderRequestHandler>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return configuration;
        }
	}
}