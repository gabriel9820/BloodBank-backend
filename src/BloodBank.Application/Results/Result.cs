namespace BloodBank.Application.Results;

public class Result(bool isSuccess = true, Error? error = default)
{
    public bool IsSuccess { get; init; } = isSuccess;
    public Error Error { get; init; } = error ?? Error.None;

    public static Result Success() => new();
    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => new(false, error);
}

public class Result<T>(T? data, bool isSuccess = true, Error? error = null) : Result(isSuccess, error)
{
    public T? Data { get; init; } = data;

    public static Result<T> Success(T data) => new(data);

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(default, false, error);
}
