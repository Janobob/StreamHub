using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace StreamHub.Api.Extensions;

/// <summary>
///     Extensions for <see cref="Result{T}" />.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Converts a <see cref="Result{T}" /> into an appropriate <see cref="ActionResult" /> based on its state.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to process.</param>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="problemDetailsFactory">The problem details factory to create responses.</param>
    /// <returns>An <see cref="ActionResult" /> representing the result.</returns>
    public static ActionResult<T> ToActionResult<T>(
        this Common.Types.Result<T> result,
        HttpContext context,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        return result.Exception switch
        {
            ArgumentException => new NotFoundObjectResult(
                problemDetailsFactory.CreateProblemDetails(
                    context,
                    StatusCodes.Status404NotFound,
                    result.Exception.Message)),

            ValidationException => new BadRequestObjectResult(
                problemDetailsFactory.CreateProblemDetails(
                    context,
                    StatusCodes.Status400BadRequest,
                    result.Exception.Message)),

            _ => new ObjectResult(problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                result.Exception!.Message))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            }
        };
    }

    /// <summary>
    ///     Converts a <see cref="Result{T}" /> into a GraphQL response or throws a GraphQL exception.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to process.</param>
    /// <returns>The result value if successful, or throws on failure.</returns>
    public static T ToGraphQlAction<T>(this Common.Types.Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Value!;
        }

        // Build a custom error if the exception is ArgumentException
        if (result.Exception is ArgumentException argEx)
        {
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(argEx.Message)
                    .SetCode("NOT_FOUND")
                    .Build());
        }

        // Fallback: Throw the original exception as a GraphQLException
        throw new GraphQLException(
            ErrorBuilder
                .New()
                .SetMessage(result.Exception?.Message ?? "An unexpected error occurred.")
                .SetCode("UNEXPECTED_ERROR")
                .Build());
    }
}