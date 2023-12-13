namespace Application.Response
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }

        public string? ErrorMessage { get; set; }

        public string? SuccessMessage { get; set; }

        public static Result<T> Success(T data, string? successMessage = null)
        {
            return new Result<T>
            {
                Data = data,
                IsSuccess = true,
                SuccessMessage = successMessage
            };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
