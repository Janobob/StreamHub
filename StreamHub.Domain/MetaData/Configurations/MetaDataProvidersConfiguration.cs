using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Domain.MetaData.Configurations;

/// <summary>
///     Configuration for the meta data providers.
/// </summary>
public class MetaDataProvidersConfiguration
{
    /// <summary>
    ///     Key for the metadata providers configuration.
    /// </summary>
    public const string Key = "MetaDataProviders";

    /// <summary>
    ///     Gets or sets the list of meta data providers.
    /// </summary>
    public List<MetaDataProviderSettings> Providers { get; set; } = [];
}