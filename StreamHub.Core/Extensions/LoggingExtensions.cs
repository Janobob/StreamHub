using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace StreamHub.Core.Extensions;

/// <summary>
///     Extension methods for <see cref="ILogger" />.
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    ///     Logs several information about the runtime and environment. This method is suitable to be called at the start of
    ///     the application.
    /// </summary>
    /// <param name="logger">The logger instance used to log runtime and environment information.</param>
    public static void LogRuntimeAndEnvironmentInformation(this ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        var runtimeInfo = RuntimeInformation.FrameworkDescription;
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        logger.LogInformation("Runtime: {Runtime}, Environment: {Environment}", runtimeInfo, environment);
    }
}