using Microsoft.Extensions.Options;
using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Services.Contracts;

namespace StreamHub.Domain.MetaData.Services;

/// <summary>
///     Resolves meta data providers and their settings.
/// </summary>
/// <param name="providers"></param>
/// <param name="providersConfiguration"></param>
public class MetaDataProviderResolver(
    IEnumerable<IMetaDataProviderService> providers,
    IOptions<MetaDataProvidersConfiguration> providersConfiguration)
    : IMetaDataProviderResolver
{
    /// <inheritdoc />
    public IEnumerable<IMetaDataProviderService> GetAllProviders()
    {
        return providers;
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">Thrown when the provider with the specified name is not found.</exception>
    public IMetaDataProviderService GetProviderByName(string name)
    {
        var provider = providers.FirstOrDefault(p => p.Name == name);

        // throw exception if provider not found
        if (provider == default)
        {
            throw new ArgumentException($"Provider with name '{name}' not found.");
        }

        return provider;
    }

    /// <inheritdoc />
    public IEnumerable<MetaDataProviderSettings> GetAllProviderSettings()
    {
        return providersConfiguration.Value.Providers;
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">Thrown when the provider with the specified name is not found.</exception>
    public MetaDataProviderSettings GetProviderSettingsByName(string name)
    {
        var settings = providersConfiguration.Value.Providers.FirstOrDefault(p => p.Name == name);

        // throw exception if provider settings not found
        if (settings == default)
        {
            throw new ArgumentException($"Provider settings with name '{name}' not found.");
        }

        return settings;
    }
}