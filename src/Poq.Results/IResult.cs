using System.Diagnostics.CodeAnalysis;

namespace Poq.Results;

/// <summary>
///     Represents the public API of an interface.
/// </summary>
public interface IResult<out T>
{
    /// <summary>
    ///     Gets a value indicating whether the result was successful.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Data))]
    [MemberNotNullWhen(false, nameof(Error))]
    bool IsSuccess { get; }

    /// <summary>
    ///     Gets a value indicating whether the result was failure.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Data))]
    [MemberNotNullWhen(true, nameof(Error))]
    bool IsFailure { get; }

    /// <summary>
    ///     Gets the error, if any.
    /// </summary>
    IResultError? Error { get; }

    /// <summary>
    ///     Gets the data, if any.
    /// </summary>
    T? Data { get; }
}
