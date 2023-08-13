namespace Innoloft.Events.Api.Models;

public class ApiResponse<T>
{
    public bool IsSuccessFul { get; set; }
    public T Data { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public ApiResponse(int statusCode,bool isSuccessFul, string message, T data)
    {
        StatusCode = statusCode;
        IsSuccessFul = isSuccessFul;
        Data = data;
        Message = message;
    }

    public ApiResponse(bool isSuccessFul, string message)
    {
        IsSuccessFul = isSuccessFul;
        Message = message;
    }

    public ApiResponse( int statusCode,bool isSuccessFul,T data)
    {
        StatusCode = statusCode;
        IsSuccessFul = isSuccessFul;
        Data = data;
    }

    public ApiResponse()
    {
    }
}