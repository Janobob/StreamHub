using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request to get a meta data provider by name.
/// </summary>
/// <param name="name">The name of the metadata provider to retrieve.</param>
public class GetMetaDataProviderRequest(string name) : IRequest<Result<MetaDataProvider>>
{
    /// <summary>
    ///     Gets the name of the metadata provider to retrieve.
    /// </summary>
    public string Name { get; init; } = name;
}