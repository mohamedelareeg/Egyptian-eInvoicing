using System.Net;

namespace BuildingBlocks.Results;
public class BaseResponse<T>
{
    public BaseResponse()
    {

    }
    public BaseResponse(T data, HttpStatusCode statusCode, string message = null)
    {
        Succeeded = true;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        Data = data;
        StatusCode = statusCode;
    }
    public BaseResponse(HttpStatusCode statusCode, string message)
    {
        Succeeded = false;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        StatusCode = statusCode;
    }
    public BaseResponse(string message, HttpStatusCode statusCode, bool succeeded)
    {
        Succeeded = succeeded;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        StatusCode = statusCode;
    }
    public BaseResponse(string message, List<string> errors, HttpStatusCode statusCode, bool succeeded)
    {
        Succeeded = succeeded;
        Errors = errors;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        StatusCode = statusCode;
    }
    public static BaseResponse<T> CreateNullDataResponse(string errorMessage, T data = default)
    {
        return new BaseResponse<T>
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Succeeded = false,
            Data = data,
            Message = errorMessage,
            Errors = new List<string> { errorMessage }
        };
    }

    public HttpStatusCode StatusCode { get; set; }
    public object Meta { get; set; }

    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public T Data { get; set; }
}
