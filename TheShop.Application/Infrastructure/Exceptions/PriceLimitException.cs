using System;

namespace TheShop.Application.Infrastructure.Exceptions
{
    public class PriceLimitException : Exception
    {
        public PriceLimitException()
        {

        }

        public PriceLimitException(string message) : base(message)
        {
            
        }
    }
}
