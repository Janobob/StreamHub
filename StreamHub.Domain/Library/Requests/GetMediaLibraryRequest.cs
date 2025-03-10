using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

/// <summary>
///     Request to get a media library by Id.
/// </summary>
/// <param name="Id">The Id of the media library to retrieve.</param>
public class GetMediaLibraryRequest(int Id) : IRequest<Result<MediaLibrary>>
{
}