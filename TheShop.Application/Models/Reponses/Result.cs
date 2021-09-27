namespace TheShop.Application.Models.Reponses
{
    public class Result<T>
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public static Result<T> Success(T data) => new Result<T>()
        {
            IsSuccessful = true,
            Data = data
        };

        public static Result<T> Error(string errorMessage) => new Result<T>()
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage
        };
    }
}
