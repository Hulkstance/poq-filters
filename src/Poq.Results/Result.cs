using System.Diagnostics.CodeAnalysis;
using Poq.Results.Errors;

namespace Poq.Results;

public readonly struct Result<T> : IResult<T>
{
    private Result(T? data)
        => (Data, Error) = (data, null);

    private Result(IResultError? error)
        => (Error) = (error);

    [MemberNotNullWhen(true, nameof(Data))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => Error is null;

    [MemberNotNullWhen(false, nameof(Data))]
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => !IsSuccess;

    public T? Data { get; }

    public IResultError? Error { get; }

    public static Result<T> FromSuccess(T data) => new(data);

    public static Result<T> FromError<TError>(TError error) where TError : IResultError
        => new(error);

    public static implicit operator Result<T>(ResultError resultError)
        => new(resultError);

    public static implicit operator Result<T>(Exception exception)
        => new(new ExceptionError(exception));
}
