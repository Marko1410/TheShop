using System;
using TheShop.Application.Services.Interfaces;

namespace TheShop.Application.Services
{
    public class Logger : ILogger
    {
		public void Info(string message)
		{
			Console.WriteLine($"Info: {message}");
		}

		public void Error(string message)
		{
			Console.WriteLine($"Error: {message}");
		}

		public void Debug(string message)
		{
			Console.WriteLine($"Debug: {message}");
		}
	}
}
