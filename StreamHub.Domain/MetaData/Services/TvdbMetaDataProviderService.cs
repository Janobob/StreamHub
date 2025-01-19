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
    private readonly HttpClient _http;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TvdbMetaDataProviderService" /> class.
    /// </summary>
    /// <param name="metaDataProviderResolver">
    ///     The resolver used to retrieve metadata provider settings based on the provider's name.
    /// </param>
    /// <param name="http">
    ///     The <see cref="HttpClient" /> instance used to make HTTP requests to the metadata provider's API.
    /// </param>
    public TvdbMetaDataProviderService(IMetaDataProviderResolver metaDataProviderResolver, HttpClient http)
    {
        _http = http;
        ProviderSettings = metaDataProviderResolver.GetProviderSettingsByName(Name);
        _http.BaseAddress = new Uri(ProviderSettings.Url);
    }

    /// <inheritdoc />
    public string Name => "Tvdb";

    /// <inheritdoc />
    public MetaDataProviderSettings ProviderSettings { get; init; }

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