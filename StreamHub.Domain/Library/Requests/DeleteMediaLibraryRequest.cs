using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

/// <summary>
///     Request to delete a media library.
/// </summary>
/// <param name="MediaLibrary">The media library to be deleted.</param>
public class DeleteMediaLibraryRequest(MediaLibrary MediaLibrary) : IRequest<Result<MediaLibrary>>
{
}