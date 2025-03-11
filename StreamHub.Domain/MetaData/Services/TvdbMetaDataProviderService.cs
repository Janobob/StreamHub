using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using StreamHub.Common.Enums;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Services.Contracts;

namespace StreamHub.Domain.MetaData.Services;

/// <summary>
///     Metadata provider service for TheTVDB.
/// </summary>
public class TvdbMetaDataProviderService : IMetaDataProviderService
{
    private readonly HttpClient _httpClient;
    private readonly MetaDataProviderConfiguration _providerConfiguration;

    private string? _token;
    private DateTime? _tokenExpiration;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TvdbMetaDataProviderService" /> class.
    /// </summary>
    /// <param name="httpClient">
    ///     The <see cref="HttpClient" /> instance used to make HTTP requests to the metadata provider's API.
    /// </param>
    /// <param name="metaDataConfiguration">The <see cref="MetaDataConfiguration" /> options for metadata providers.</param>
    public TvdbMetaDataProviderService(HttpClient httpClient, IOptions<MetaDataConfiguration> metaDataConfiguration)
    {
        _providerConfiguration = metaDataConfiguration.Value.Providers.FirstOrDefault(p => p.Name == Name) ??
                                 throw new ArgumentException("Provider configuration not found with name 'Tvdb'.");

        // set base URL
        var url = _providerConfiguration.Url;
        httpClient.BaseAddress = new Uri(url);

        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public string Name => "Tvdb";

    /// <inheritdoc />
    public Task<Result<MediaMetaData?>> GetMediaMetaDataAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Result<MovieMetaData?>> GetMovieMetaDataAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Result<SeriesMetaData?>> GetSeriesMetaDataAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<MetaDataSearchResult>>> SearchMediaAsync(string query,
        int limit,
        MediaType type = MediaType.All)
    {
        // get token
        var tokenResult = await GetTokenAsync();

        return await tokenResult.Combine(async token =>
        {
            // set token in request headers
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // build request URL
            var responseUrl = $"/v4/search?q={query}&limit={limit}";
            if (type != MediaType.All)
            {
                // todo: check if a mapping between MediaType and TheTVDB type is needed
                responseUrl += $"&type={type.ToString().ToLower()}";
            }

            // perform get request to search
            var data = await _httpClient.GetFromJsonAsync<JsonElement>(responseUrl);
            if (!data.TryGetProperty("data", out var dataArray) || dataArray.ValueKind != JsonValueKind.Array)
            {
                return Result<IEnumerable<MetaDataSearchResult>>.Failure(
                    new Exception("Failed to search TheTVDB. Error: No response."));
            }

            var results = new List<MetaDataSearchResult>();
            foreach (var item in dataArray.EnumerateArray())
            {
                var providerIds = new Dictionary<string, string>();
                if (item.TryGetProperty("tvdb_id", out var tvdbId) && tvdbId.ValueKind == JsonValueKind.String)
                {
                    providerIds.Add(Name, tvdbId.GetString()!);
                }

                // TODO: parse media type
                var mediaType = MediaType.All;

                if (item.TryGetProperty("name", out var name) && name.ValueKind == JsonValueKind.String &&
                    item.TryGetProperty("overview", out var overview) && overview.ValueKind == JsonValueKind.String)
                {
                    results.Add(new MetaDataSearchResult
                    {
                        ProviderIds = providerIds,
                        Name = name.GetString() ?? "Unknown",
                        Description = overview.GetString() ?? "No description available",
                        MediaType = mediaType
                    });
                }
            }

            return Result<IEnumerable<MetaDataSearchResult>>.Success(results);
        });
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<MetaDataSearchResult>>> SearchMoviesAsync(string query,
        int limit)
    {
        return await SearchMediaAsync(query, limit, MediaType.Movie);
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<MetaDataSearchResult>>> SearchSeriesAsync(string query,
        int limit)
    {
        return await SearchMediaAsync(query, limit, MediaType.Series);
    }

    private async Task<Result<string>> GetTokenAsync()
    {
        // check if token is still valid
        if (_token is not null && _tokenExpiration is not null && _tokenExpiration > DateTime.Now)
        {
            return Result<string>.Success(_token);
        }

        // perform post request to login
        var request =
            await _httpClient.PostAsJsonAsync("/v4/login",
                new { apiKey = _providerConfiguration.ApiKey });

        // check if request failed
        if (!request.IsSuccessStatusCode)
        {
            var error = await request.Content.ReadAsStringAsync();
            return Result<string>.Failure(new Exception("Failed to login to TheTVDB. Error: " + error));
        }

        // read response and check if it's null
        var response = await request.Content.ReadFromJsonAsync<JsonElement>();
        if (!response.TryGetProperty("data", out var data) || !data.TryGetProperty("token", out var tokenElement))
        {
            return Result<string>.Failure(new Exception("Failed to login to TheTVDB. Error: No response."));
        }

        // store token and expiration date 14 days from now
        _token = tokenElement.GetString();
        _tokenExpiration = DateTime.Now.AddDays(14);

        return Result<string>.Success(_token!);
    }
}