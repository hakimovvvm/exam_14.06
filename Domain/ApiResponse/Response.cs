using System.Net;

namespace Domain.ApiResponse;

public class Response<T>
{
    private string v;
    private HttpStatusCode badRequest;

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public Response(T data, string? message = null!)
    {
        IsSuccess = true;
        Message = message;
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
    }
    public Response(HttpStatusCode statusCode, string? message = null!)
    {
        IsSuccess = false;
        Message = message;
        Data = default;
        StatusCode = (int)statusCode;
    }
}
