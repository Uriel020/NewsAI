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


    //Results for Http Errors

    public static Result<T> BadRequest(string error) => new(default!, false, error); //400 BadRequest

    public static Result<T> Unauthorized(string error) => new(default!, false, error); //401 Unauthorized

    public static Result<T> Forbidden(string error) => new(default!, false, error); //403 Forbidden 

    public static Result<T> NotFound(string error) => new(default!, false, error); //404 Not Found

    public static Result<T> Conflict(string error = "Resource Not Found") => new(default!, false, error); //409 Conflict





}