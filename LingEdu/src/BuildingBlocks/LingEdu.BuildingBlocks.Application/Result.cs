namespace LingEdu.BuildingBlocks.Application;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, Error? error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static new Result<T> Failure(Error error) => new(false, default, error);
}

public record Error(string Code, string Message)
{
    public static Error NotFound(string message) => new("not_found", message);
    public static Error Validation(string message) => new("validation_error", message);
    public static Error Unauthorized(string message) => new("unauthorized", message);
}
