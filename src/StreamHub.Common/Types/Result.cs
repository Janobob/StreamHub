namespace StreamHub.Common.Types;

/// <summary>
///     Represents the result of an operation that can either succeed or fail.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public class Result<T>
{
    /// <summary>
    ///     Gets the value of the result if the operation was successful.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    ///     Gets the exception if the operation failed.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    ///     Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess => Exception == null;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Result{T}" /> class for a successful operation.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    private Result(T value)
    {
        Value = value;
        Exception = null;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Result{T}" /> class for a failed operation.
    /// </summary>
    /// <param name="exception">The exception representing the failure.</param>
    private Result(Exception exception)
    {
        Value = default;
        Exception = exception;
    }

    /// <summary>
    ///     Creates a successful result.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    /// <returns>A <see cref="Result{T}" /> representing a successful operation.</returns>
    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    /// <summary>
    ///     Creates a failed result.
    /// </summary>
    /// <param name="exception">The exception representing the failure.</param>
    /// <returns>A <see cref="Result{T}" /> representing a failed operation.</returns>
    public static Result<T> Failure(Exception exception)
    {
        return new Result<T>(exception);
    }

    /// <summary>
    ///     Transforms the value inside a successful result.
    /// </summary>
    /// <typeparam name="TResult">The type of the transformed value.</typeparam>
    /// <param name="transform">The function to apply to the value.</param>
    /// <returns>A new <see cref="Result{T}" /> containing the transformed value or the original failure.</returns>
    public Result<TResult> Map<TResult>(Func<T, TResult> transform)
    {
        if (IsSuccess)
        {
            return Result<TResult>.Success(transform(Value!));
        }

        return Result<TResult>.Failure(Exception!);
    }

    /// <summary>
    ///     Chains another operation based on the current result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of the next operation.</typeparam>
    /// <param name="next">The function to execute if the operation succeeded.</param>
    /// <returns>A new <see cref="Result{T}" /> from the chained operation.</returns>
    public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> next)
    {
        if (IsSuccess)
        {
            return next(Value!);
        }

        return Result<TResult>.Failure(Exception!);
    }

    /// <summary>
    ///     Matches the result and returns a value based on success or failure.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <param name="onSuccess">The function to execute if the operation succeeded.</param>
    /// <param name="onFailure">The function to execute if the operation failed.</param>
    /// <returns>The result of either <paramref name="onSuccess" /> or <paramref name="onFailure" />.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Exception, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Exception!);
    }

    /// <summary>
    ///     Combines this result with another result into a single result containing both values.
    /// </summary>
    /// <typeparam name="TOther">The type of the other result.</typeparam>
    /// <param name="other">The other result to combine with.</param>
    /// <returns>A new <see cref="Result{T}" /> containing a tuple of the two results or the first failure encountered.</returns>
    public Result<(T, TOther)> Combine<TOther>(Result<TOther> other)
    {
        if (!IsSuccess)
        {
            return Result<(T, TOther)>.Failure(Exception!);
        }

        return !other.IsSuccess
            ? Result<(T, TOther)>.Failure(other.Exception!)
            : Result<(T, TOther)>.Success((Value!, other.Value!));
    }

    /// <summary>
    ///     Combines this result with an asynchronous operation that returns another result,
    ///     producing a single result containing both values.
    /// </summary>
    /// <typeparam name="TOther">The type of the result from the next operation.</typeparam>
    /// <param name="next">A function that takes the current result value and returns a new async result.</param>
    /// <returns>
    ///     A new <see cref="Result{T}" /> containing a tuple of both values if both operations succeed,
    ///     or the first encountered failure.
    /// </returns>
    public async Task<Result<TOther>> Combine<TOther>(
        Func<T, Task<Result<TOther>>> next)
    {
        if (!IsSuccess)
        {
            return Result<TOther>.Failure(Exception!);
        }

        return await next(Value!);
    }

    /// <summary>
    ///     Converts the result to a string representation.
    /// </summary>
    /// <returns>A string that represents the result.</returns>
    public override string ToString()
    {
        return IsSuccess ? $"Success: {Value}" : $"Failure: {Exception?.Message}";
    }
}