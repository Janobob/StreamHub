using Microsoft.Extensions.Options;
using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Services.Contracts;
using StreamHub.Persistence.Enums;

namespace StreamHub.Domain.MetaData.Services;

/// <summary>
///     Metadata provider service for TheTVDB.
/// </summary>
public class TvdbMetaDataProviderService : IMetaDataProviderService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<MetaDataConfiguration> _metaDataConfiguration;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TvdbMetaDataProviderService" /> class.
    /// </summary>
    /// <param name="httpClient">
    ///     The <see cref="HttpClient" /> instance used to make HTTP requests to the metadata provider's API.
    /// </param>
    /// <param name="metaDataConfiguration">The <see cref="MetaDataConfiguration" /> options for metadata providers.</param>
    public TvdbMetaDataProviderService(HttpClient httpClient, IOptions<MetaDataConfiguration> metaDataConfiguration)
    {
        _httpClient = httpClient;
        _metaDataConfiguration = metaDataConfiguration;
    }

    /// <inheritdoc />
    public string Name => "Tvdb";

    /// <inheritdoc />
    public Task<IEnumerable<MediaMetaDataSearchResult>> SearchMediaAsync(string query, int limit = 10)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<MediaMetaDataSearchResult>> SearchMediaAsync(string query, MediaType type, int limit = 10)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<MediaMetaDataSearchResult>> SearchMoviesAsync(string query, int limit = 10)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<MediaMetaDataSearchResult>> SearchSeriesAsync(string query, int limit = 10)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<MediaMetaData?> GetMediaMetaDataAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<MovieMetaData?> GetMovieMetaDataAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<SeriesMetaData?> GetSeriesMetaDataAsync(int id)
    {
        throw new NotImplementedException();
    }
}