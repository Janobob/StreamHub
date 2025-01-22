namespace StreamHub.Api.Middlewares;

/// <summary>
///     Middleware to log HTTP request and response details.
/// </summary>
/// <param name="next">
///     The next middleware in the request pipeline.
/// </param>
/// <param name="logger">
///     The logger used to log request and response details.
/// </param>
public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    /// <summary>
    ///     Invokes the middleware to handle logging of HTTP requests and responses.
    /// </summary>
    /// <param name="context">
    ///     The <see cref="HttpContext" /> representing the current HTTP request and response context.
    /// </param>
    /// <returns>
    ///     A <see cref="Task" /> representing the asynchronous operation.
    /// </returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // Log request details
        logger.LogInformation("Request: {Method} {Path}",
            context.Request.Method, context.Request.Path);

        // Capture the response body for logging
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        // Log response details
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        logger.LogInformation("Response: {StatusCode} {Body}",
            context.Response.StatusCode, responseText);

        await responseBody.CopyToAsync(originalBodyStream);
    }
}