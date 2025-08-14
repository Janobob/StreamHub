using MediatR;
using StreamHub.Api.Hubs.Notifiers;
using StreamHub.Api.MediatR;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Services;
using StreamHub.Domain.Library.Services.Contracts;
using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Services;
using StreamHub.Domain.MetaData.Services.Contracts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Api.Extensions;

/// <summary>
///     Extension methods for dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Registers all configurations.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>
    ///     The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<MetaDataConfiguration>()
            .Bind(configuration.GetSection(MetaDataConfiguration.Key))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    /// <summary>
    ///     Registers all repository implementations as scoped services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>
    ///     The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMediaLibraryRepository, MediaLibraryRepository>();
        services.AddScoped<IMediaRepository<MediaEntity>, MediaRepository>();
        services.AddScoped<IMediaRepository<SeriesEntity>, SeriesRepository>();
        services.AddScoped<IMediaRepository<MovieEntity>, MovieRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ISeriesRepository, SeriesRepository>();
        services.AddScoped<IEpisodeRepository, EpisodeRepository>();

        return services;
    }

    /// <summary>
    ///     Registers all metadata providers and services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>
    ///     The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddMetadataProvidersAndServices(this IServiceCollection services)
    {
        services.AddHttpClient<IMetaDataProviderService, TvdbMetaDataProviderService>();
        services.AddScoped<IMetaDataProviderResolver, MetaDataProviderResolver>();

        return services;
    }

    /// <summary>
    ///     Registers all library services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>
    ///     The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddLibraryServices(this IServiceCollection services)
    {
        services.AddScoped<IMediaLibraryService, MediaLibraryService>();
        services.AddAutoMapper(cfg => cfg.LicenseKey = "", typeof(LibraryMappingProfile));

        return services;
    }

    /// <summary>
    ///     Registers MediatR services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>
    ///     The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.
    /// </returns>
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        // Register all MediatR services from the current domain assemblies
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()); });

        // Decorate the IMediator service with a logging decorator
        services.Decorate<IMediator>((inner, provider) =>
        {
            var logger = provider.GetRequiredService<ILogger<LoggingMediator>>();
            return new LoggingMediator(inner, logger);
        });

        return services;
    }

    public static IServiceCollection AddSignalRServices(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<ILibraryHubNotifier, LibraryHubNotifier>();

        return services;
    }
}