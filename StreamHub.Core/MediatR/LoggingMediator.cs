using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StreamHub.Core.MediatR;

/// <summary>
///     A decorator for <see cref="IMediator" /> that adds logging for all MediatR requests and notifications.
/// </summary>
/// <param name="inner">The inner <see cref="IMediator" /> instance to decorate.</param>
/// <param name="logger">The logger instance used for logging.</param>
public class LoggingMediator(IMediator inner, ILogger<LoggingMediator> logger) : IMediator
{
    private readonly JsonSerializerSettings _jsonSettings = new()
    {
        Formatting = Formatting.None,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        }
    };

    /// <inheritdoc />
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Sending request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonConvert.SerializeObject(request, _jsonSettings));

        var response = await inner.Send(request, cancellationToken);

        logger.LogInformation("Request processed: {RequestName} with response: {ResponseJson}",
            request.GetType().Name,
            JsonConvert.SerializeObject(response, _jsonSettings));

        return response;
    }

    /// <inheritdoc />
    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = new()) where TRequest : IRequest
    {
        logger.LogInformation("Sending request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonConvert.SerializeObject(request, _jsonSettings));

        return inner.Send(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<object?> Send(object request, CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Sending request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonConvert.SerializeObject(request, _jsonSettings));

        return inner.Send(request, cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Creating stream for request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonConvert.SerializeObject(request, _jsonSettings));

        return inner.CreateStream(request, cancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Creating stream for request: {RequestName} with data: {RequestJson}",
            request.GetType().Name,
            JsonConvert.SerializeObject(request, _jsonSettings));

        return inner.CreateStream(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task Publish(object notification, CancellationToken cancellationToken = new())
    {
        logger.LogInformation("Publishing notification: {NotificationName} with data: {NotificationJson}",
            notification.GetType().Name,
            JsonConvert.SerializeObject(notification, _jsonSettings));

        return inner.Publish(notification, cancellationToken);
    }

    /// <inheritdoc />
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new())
        where TNotification : INotification
    {
        logger.LogInformation("Publishing notification: {NotificationName} with data: {NotificationJson}",
            typeof(TNotification).Name,
            JsonConvert.SerializeObject(notification, _jsonSettings));

        return inner.Publish(notification, cancellationToken);
    }
}