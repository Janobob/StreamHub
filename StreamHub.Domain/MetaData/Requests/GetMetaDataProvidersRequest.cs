using MediatR;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request to get all meta data providers.
/// </summary>
public class GetMetaDataProvidersRequest : IRequest<IEnumerable<MetaDataProvider>>
{
}