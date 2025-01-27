using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using StreamHub.Common.Types;

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
        this Result<T> result,
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
}