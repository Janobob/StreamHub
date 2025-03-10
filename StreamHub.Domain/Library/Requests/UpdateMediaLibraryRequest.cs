using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

/// <summary>
///     Request to update a media library.
/// </summary>
/// <param name="MediaLibrary">The media library to be updated.</param>
public class UpdateMediaLibraryRequest(MediaLibrary MediaLibrary) : IRequest<Result<MediaLibrary>>
{
}