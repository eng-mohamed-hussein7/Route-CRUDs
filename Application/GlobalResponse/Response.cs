using Application.ResultFolder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.GlobalResponse;

public enum ResponseStatus
{
    Success,
    Created,
    Failed,
    NotFound,
    ValidationError,
}
public class Response
{
    public bool Succeeded { get; private set; }
    public ResponseStatus Status { get; private set; }
    public string? Message { get; private set; }
    public Error? Error { get; private set; }
    public object Data { get; private set; }

    private Response(bool succeeded, ResponseStatus status, string? message, Error? error, object data)
    {
        Succeeded = succeeded;
        Status = status;
        Message = message;
        Error = error;
        Data = data;
    }

    public static Response Success(object data = default, string? message = null)
        => new(true, ResponseStatus.Success, message, null, data);

    public static Response Created(object data = default, string? message = null)
        => new(true, ResponseStatus.Created, message, null, data);

    public static Response Failure(string? message, ResponseStatus status = ResponseStatus.Failed, Error? error = null)
        => new(false, status, message, error, default);

    public static Response NotFound(string? message = null, Error? error = null)
        => new(false, ResponseStatus.NotFound, message ?? "Resource not found.", error, default);

    public static Response Validation(string? message, Error? error = null)
        => new(false, ResponseStatus.ValidationError, message, error, default);
}