namespace StreamHub.Domain.MetaData.Configurations;

/// <summary>
///     Configuration item for a meta data provider.
/// </summary>
public sealed class MetaDataProviderConfiguration
{
    /// <summary>
    ///     Gets or sets the name of the meta data provider.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Gets or sets the URL of the meta data provider.
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    ///     Gets or sets the API key for the meta data provider.
    /// </summary>
    public required string ApiKey { get; set; }
}