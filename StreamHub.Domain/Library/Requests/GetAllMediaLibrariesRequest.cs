using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

/// <summary>
///     Request to get all media libraries.
/// </summary>
public record GetAllMediaLibrariesRequest : IRequest<Result<IEnumerable<MediaLibrary>>>
{
}