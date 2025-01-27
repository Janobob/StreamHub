using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace StreamHub.Core.MediatR;

/// <summary>
///     A decorator for <see cref="IMediator" /> that adds logging for all MediatR requests and notifications.
/// </summary>
/// <param name="inner">The inner <see cref="IMediator" /> instance to decorate.</param>
/// <param name="logger">The logger instance used for logging.</param>
public class LoggingMediator(IMediator inner, ILogger<LoggingMediator> logger) : IMediator
{
    /// <inheritdoc />
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Sending request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonSerializer.Serialize(request));

        var response = await inner.Send(request, cancellationToken);

        logger.LogInformation("Request processed: {RequestName} with response: {ResponseJson}",
            request.GetType().Name,
            JsonSerializer.Serialize(response));

        return response;
    }

    /// <inheritdoc />
    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = new()) where TRequest : IRequest
    {
        logger.LogInformation("Sending request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonSerializer.Serialize(request));

        return inner.Send(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<object?> Send(object request, CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Sending request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonSerializer.Serialize(request));

        return inner.Send(request, cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Creating stream for request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonSerializer.Serialize(request));

        return inner.CreateStream(request, cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Creating stream for request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonSerializer.Serialize(request));

        return inner.CreateStream(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task Publish(object notification, CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Publishing notification: {NotificationName} with data: {NotificationJson}",
            notification.GetType().Name,
            JsonSerializer.Serialize(notification));

        return inner.Publish(notification, cancellationToken);
    }

    /// <inheritdoc />
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new())
        where TNotification : INotification
    {
        logger.LogInformation("Publishing notification: {NotificationName} with data: {NotificationJson}",
            typeof(TNotification).Name,
            JsonSerializer.Serialize(notification));

        return inner.Publish(notification, cancellationToken);
    }
}