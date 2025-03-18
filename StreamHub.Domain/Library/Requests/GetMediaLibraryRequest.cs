using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

/// <summary>
///     Request to get a media library by Id.
/// </summary>
/// <param name="Id">The ID of the media library to retrieve.</param>
public record GetMediaLibraryRequest(int Id) : IRequest<Result<MediaLibrary>>
{
}