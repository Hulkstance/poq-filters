namespace Poq.Results.Errors;

/// <summary>
///     Represents an error caused by an exception.
/// </summary>
public sealed record TimeoutError() : ResultError(null, "Timeout");
