using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request to get a meta data provider by name.
/// </summary>
/// <param name="Name">The name of the metadata provider to retrieve.</param>
public record GetMetaDataProviderRequest(string Name) : IRequest<Result<MetaDataProvider>>
{
}