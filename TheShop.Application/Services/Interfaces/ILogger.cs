namespace TheShop.Application.Services.Interfaces
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
        void Debug(string message);
    }
}
