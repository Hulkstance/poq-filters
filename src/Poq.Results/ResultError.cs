namespace Poq.Results;

/// <summary>
///     Acts as a base class for result errors.
/// </summary>
public abstract record ResultError(int? Code, string Message) : IResultError;
