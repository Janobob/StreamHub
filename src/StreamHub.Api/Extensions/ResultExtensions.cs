using System.ComponentModel.DataAnnotations;
using AutoMapper;
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
    ///     Converts a <see cref="Result{T}" /> into an appropriate <see cref="IActionResult" /> with No Content (204) status.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to process.</param>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="problemDetailsFactory">The problem details factory to create responses.</param>
    /// <returns>
    ///     A <see cref="NoContentResult" /> if successful, otherwise an appropriate error response as
    ///     <see cref="IActionResult" />.
    /// </returns>
    public static IActionResult ToNoContentActionResult<T>(
        this Common.Types.Result<T> result,
        HttpContext context,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess)
        {
            return new NoContentResult();
        }

        // return result or throw exception if not successful
        return result.ToActionResult(context, problemDetailsFactory).Result ??
               new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    ///     Converts a <see cref="Result{T}" /> into an appropriate <see cref="ActionResult" /> with Created (201) status.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to process.</param>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="problemDetailsFactory">The problem details factory to create responses.</param>
    /// <returns>
    ///     A <see cref="CreatedResult" /> with the new resource if successful, otherwise an appropriate error response.
    /// </returns>
    public static ActionResult<T> ToCreatedActionResult<T>(
        this Common.Types.Result<T> result,
        HttpContext context,
        ProblemDetailsFactory problemDetailsFactory)
    {
        if (result.IsSuccess)
        {
            return new CreatedResult(context.Request.Path, result.Value);
        }

        // passthrough to ToActionResult to get error response
        return result.ToActionResult(context, problemDetailsFactory);
    }

    /// <summary>
    ///     Converts a <see cref="Result{T}" /> into an appropriate <see cref="ActionResult" /> with OK (200) status.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to process.</param>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="problemDetailsFactory">The problem details factory to create responses.</param>
    /// <returns>
    ///     A <see cref="OkObjectResult" /> with resource if successful, otherwise an appropriate error response.
    /// </returns>
    public static ActionResult<T> ToOkActionResult<T>(
        this Common.Types.Result<T> result,
        HttpContext context,
        ProblemDetailsFactory problemDetailsFactory)
    {
        // acts as a pass-through for the ToActionResult method, may change in the future
        return result.ToActionResult(context, problemDetailsFactory);
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

    /// <summary>
    ///     Maps the value inside a successful <see cref="Result{TSource}" /> to a new type
    ///     <typeparamref name="TDestination" />
    ///     using AutoMapper. If the result is a failure, the failure is propagated unchanged.
    /// </summary>
    /// <typeparam name="TSource">The type of the original value.</typeparam>
    /// <typeparam name="TDestination">The type to map the value to.</typeparam>
    /// <param name="result">The result containing the value to transform.</param>
    /// <param name="mapper">The AutoMapper instance used for mapping.</param>
    /// <returns>
    ///     A new <see cref="Result{TDestination}" /> containing the mapped value if the operation succeeded,
    ///     or the original failure if the operation failed.
    /// </returns>
    public static Common.Types.Result<TDestination> Map<TSource, TDestination>(
        this Common.Types.Result<TSource> result,
        IMapper mapper)
    {
        return result.IsSuccess
            ? Common.Types.Result<TDestination>.Success(mapper.Map<TDestination>(result.Value!))
            : Common.Types.Result<TDestination>.Failure(result.Exception!);
    }

    /// <summary>
    ///     Maps the values inside a successful
    ///     <see>
    ///         <cref>Result{IEnumerable{TSource}}</cref>
    ///     </see>
    ///     to a list of
    ///     <typeparamref name="TDestination" />
    ///     using AutoMapper. If the result is a failure, the failure is propagated unchanged.
    /// </summary>
    /// <typeparam name="TSource">The type of the original list elements.</typeparam>
    /// <typeparam name="TDestination">The type to map the list elements to.</typeparam>
    /// <param name="result">The result containing the list of values to transform.</param>
    /// <param name="mapper">The AutoMapper instance used for mapping.</param>
    /// <returns>
    ///     A new
    ///     <see>
    ///         <cref>Result{List{TDestination}}</cref>
    ///     </see>
    ///     containing the mapped list if the operation succeeded,
    ///     or the original failure if the operation failed.
    /// </returns>
    public static Common.Types.Result<IEnumerable<TDestination>> MapList<TSource, TDestination>(
        this Common.Types.Result<IEnumerable<TSource>> result,
        IMapper mapper)
    {
        return result.IsSuccess
            ? Common.Types.Result<IEnumerable<TDestination>>.Success(
                mapper.Map<IEnumerable<TDestination>>(result.Value!))
            : Common.Types.Result<IEnumerable<TDestination>>.Failure(result.Exception!);
    }
}