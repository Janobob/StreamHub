using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Domain.MetaData.Services.Contracts;

/// <summary>
///     Resolver interface for metadata provider services.
/// </summary>
public interface IMetaDataProviderResolver
{
    /// <summary>
    ///     Get all metadata provider services.
    /// </summary>
    /// <returns>An enumerable collection of all metadata provider services.</returns>
    IEnumerable<IMetaDataProviderService> GetAllProviderServices();

    /// <summary>
    ///     Get all metadata providers.
    /// </summary>
    /// <returns>An enumerable collection of all metadata provider.</returns>
    IEnumerable<MetaDataProvider> GetAllProvider();

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
}