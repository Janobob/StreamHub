using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Persistence.Enums;

namespace StreamHub.Domain.MetaData.Services.Contracts;

/// <summary>
///     Resolver interface for metadata provider services.
/// </summary>
public interface IMetaDataProviderResolver
{
    /// <summary>
    ///     Get all metadata providers.
    /// </summary>
    /// <returns>A result containing a collection of all metadata provider.</returns>
    Result<IEnumerable<MetaDataProvider>> GetAllProviders();

    /// <summary>
    ///     Get metadata provider by name.
    /// </summary>
    /// <param name="name">
    ///     The name of the metadata provider.
    /// </param>
    /// <returns>
    ///     A result containing the metadata provider with the specified name.
    /// </returns>
    Result<MetaDataProvider> GetProviderByName(string name);

    /// <summary>
    ///     Get all metadata provider services.
    /// </summary>
    /// <returns>An enumerable collection of all metadata provider services.</returns>
    IEnumerable<IMetaDataProviderService> GetAllProviderServices();

    /// <summary>
    ///     Get metadata provider service by name.
    /// </summary>
    /// <param name="name">The name of the metadata provider.</param>
    /// <returns>The metadata provider service with the specified name.</returns>
    IMetaDataProviderService GetProviderServiceByName(string name);

    /// <summary>
    ///     Get all metadata provider settings.
    /// </summary>
    /// <returns>An enumerable collection of all metadata provider settings.</returns>
    IEnumerable<MetaDataProviderConfiguration> GetAllProviderSettings();

    /// <summary>
    ///     Get metadata provider settings by name.
    /// </summary>
    /// <param name="name">The name of the metadata provider</param>
    /// <returns>The metadata provider settings with the specified name.</returns>
    MetaDataProviderConfiguration GetProviderSettingsByName(string name);

    # region Search

    /// <summary>
    ///     Searches for media items of a specific type based on the provided query.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="name">
    ///     The name of the provider service to be executed, when 'null' it does use all of the registered
    ///     provider services.
    /// </param>
    /// <param name="limit">The maximum number of search results to return. Default is 10.</param>
    /// <param name="type">The type of media to search for (e.g., Movie, Series).</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an
    ///     <see cref="IEnumerable{T}" /> of <see cref="MetaDataSearchResult" /> representing the search results.
    /// </returns>
    Task<Result<IEnumerable<MetaDataSearchResult>>> SearchMediaAsync(string query, string? name, int limit = 10,
        MediaType type = MediaType.All);

    # endregion
}