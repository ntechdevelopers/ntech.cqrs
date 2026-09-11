namespace Ntech.CQRS.Application.Common;

public class Response<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? ErrorDetails { get; set; }

    public static Response<T> Ok(T data, string message = "Success")
    {
        return new Response<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static Response<T> Fail(string message, List<string>? errorDetails = null)
    {
        return new Response<T>
        {
            Success = false,
            Message = message,
            ErrorDetails = errorDetails
        };
    }
}
