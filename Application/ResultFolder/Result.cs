using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.ResultFolder;

public enum StatusResult
{
    Failed,  
    Success,
    Exist,
    NotExists
}


public class Result
{
    public bool Succeeded { get; private set; }
    public StatusResult Status { get; private set; }
    public string? Message { get; private set; }
    public object? Data { get; private set; }
    public Error? Error { get; private set; }

    private Result(
        bool succeeded,
        StatusResult status,
        string? message,
        Error? error,
        object? data)
    {
        Succeeded = succeeded;
        Status = status;
        Message = message;
        Error = error;
        Data = data;
    }

    public static Result Success(
        string? message = null,
        object? data = null)
        => new(true, StatusResult.Success, message, null, data);

    public static Result Failure(
        string? message,
        Error? error = null,
        StatusResult status = StatusResult.Failed,
        object? data = null)
        => new(false, status, message, error, data);

    public static Result NotExists(
        string? message,
        Error? error = null,
        StatusResult status = StatusResult.NotExists,
        object? data = null)
        => new(false, status, message, error, data);
}
