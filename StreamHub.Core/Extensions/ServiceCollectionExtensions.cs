using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Services;
using StreamHub.Domain.MetaData.Services.Contracts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Core.Extensions;

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
        services.Configure<MetaDataConfiguration>(
            configuration.GetSection(MetaDataConfiguration.Key));

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
        services.AddScoped<IMediaRepository<Media>, MediaRepository>();
        services.AddScoped<IMediaRepository<Series>, SeriesRepository>();
        services.AddScoped<IMediaRepository<Movie>, MovieRepository>();
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
}