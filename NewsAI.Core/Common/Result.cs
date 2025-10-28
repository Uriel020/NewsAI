using FluentValidation.Results;

namespace NewsAI.Core.Common;

public class Result<T>
{
    public T Value { get; }
    public bool IsSuccess { get; }
    public string? Error { get; }

    public List<ValidationFailure>? Errors { get; } = new List<ValidationFailure>();


    private Result(T value, bool isSuccess, string? error, List<ValidationFailure>? errors = null)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new(value, true, null);

    public static Result<T> Failure(string error) => new(default!, false, error);

    public static Result<T> Failure(List<ValidationFailure> errors) => new(default!, false, null, errors);


}