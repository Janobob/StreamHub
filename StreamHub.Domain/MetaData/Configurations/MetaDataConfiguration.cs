namespace StreamHub.Domain.MetaData.Configurations;

/// <summary>
///     Configuration for the meta data providers.
/// </summary>
public sealed class MetaDataConfiguration
{
    /// <summary>
    ///     Key for the metadata providers configuration.
    /// </summary>
    public const string Key = "MetaData";

    /// <summary>
    ///     Gets or sets the list of meta data providers.
    /// </summary>
    public List<MetaDataProviderConfiguration> Providers { get; set; } = [];
}