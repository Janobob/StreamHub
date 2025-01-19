using Microsoft.Extensions.DependencyInjection;
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
    ///     Registers all repository implementations as scoped services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMediaLibraryRepository, MediaLibraryRepository>();
        services.AddScoped<IMediaRepository<Media>, MediaRepository>();
        services.AddScoped<IMediaRepository<Series>, SeriesRepository>();
        services.AddScoped<IMediaRepository<Movie>, MovieRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ISeriesRepository, SeriesRepository>();
    }
}