using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Domain.MetaData.Requests;

/// <summary>
///     Request to get all meta data providers.
/// </summary>
public class GetMetaDataProvidersRequest : IRequest<Result<IEnumerable<MetaDataProvider>>>
{
}