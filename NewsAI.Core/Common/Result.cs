using FluentValidation.Results;

namespace NewsAI.Core.Common;

public class Result<T>
{
    public T Value { get; }
    public bool IsSuccess { get; }
    public string? Error { get; }
    public HttpErrorType HttpErrorType { get; }
    public List<ValidationFailure>? Errors { get; }


    private Result(
        T value,
        bool isSuccess,
        string? error,
        HttpErrorType httpErrorType = HttpErrorType.None,
        List<ValidationFailure>? errors = null)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
        HttpErrorType = httpErrorType;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new(value, true, null);

    public static Result<T> Failure(string error) => new(default!, false, error);

    public static Result<T> Failure(List<ValidationFailure> errors) => new(default!, false, null, HttpErrorType.None, errors);


    //Results for Http Errors

    public static Result<T> BadRequest(string error) => new(default!, false, error, HttpErrorType.BadRequest); //400 BadRequest

    public static Result<T> Unauthorized(string error = "Invalid access") => new(default!, false, error, HttpErrorType.Unauthorized); //401 Unauthorized

    public static Result<T> Forbidden(string error) => new(default!, false, error, HttpErrorType.Forbidden); //403 Forbidden 

    public static Result<T> NotFound(string error = "Resource Not Found") => new(default!, false, error, HttpErrorType.NotFound); //404 Not Found

    public static Result<T> Conflict(string error) => new(default!, false, error, HttpErrorType.Conflict); //409 Conflict





}