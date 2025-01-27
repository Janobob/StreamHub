﻿using Microsoft.Extensions.Options;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Configurations;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Services.Contracts;

namespace StreamHub.Domain.MetaData.Services;

/// <summary>
///     Resolves meta data providers and their settings.
/// </summary>
/// <param name="providers">
///     A collection of <see cref="IMetaDataProviderService" /> used to resolve meta data providers.
/// </param>
/// <param name="providersConfiguration">
///     Configuration options for meta data providers, represented by <see cref="MetaDataConfiguration" />.
/// </param>
public class MetaDataProviderResolver(
    IEnumerable<IMetaDataProviderService> providers,
    IOptions<MetaDataConfiguration> providersConfiguration)
    : IMetaDataProviderResolver
{
    /// <inheritdoc />
    public IEnumerable<IMetaDataProviderService> GetAllProviderServices()
    {
        return providers;
    }

    /// <inheritdoc />
    public Result<IEnumerable<MetaDataProvider>> GetAllProviders()
    {
        // return all available meta data providers
        return Result<IEnumerable<MetaDataProvider>>.Success(providersConfiguration.Value.Providers
            .Select(p =>
                new MetaDataProvider
                {
                    Name = p.Name
                }));
    }

    /// <inheritdoc />
    public Result<MetaDataProvider> GetProviderByName(string name)
    {
        // get provider configuration by name
        var config = providersConfiguration.Value.Providers.FirstOrDefault(p => p.Name == name);

        // throw exception if provider not found
        if (config == default)
        {
            var ex = new ArgumentException($"Provider with name '{name}' not found.");
            return Result<MetaDataProvider>.Failure(ex);
        }

        return Result<MetaDataProvider>.Success(new MetaDataProvider
        {
            Name = config.Name
        });
    }

    /// <inheritdoc />
    public IEnumerable<MetaDataProviderConfiguration> GetAllProviderSettings()
    {
        return providersConfiguration.Value.Providers;
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">Thrown when the provider with the specified name is not found.</exception>
    public MetaDataProviderConfiguration GetProviderSettingsByName(string name)
    {
        // get provider settings by name
        var settings = providersConfiguration.Value.Providers.FirstOrDefault(p => p.Name == name);

        // throw exception if provider settings not found
        if (settings == default)
        {
            throw new ArgumentException($"Provider settings with name '{name}' not found.");
        }

        return settings;
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">Thrown when the provider with the specified name is not found.</exception>
    public IMetaDataProviderService GetProviderServiceByName(string name)
    {
        // get provider by name
        var provider = providers.FirstOrDefault(p => p.Name == name);

        // throw exception if provider not found
        if (provider == default)
        {
            throw new ArgumentException($"Provider with name '{name}' not found.");
        }

        return provider;
    }
}