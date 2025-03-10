using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

/// <summary>
///     Request to add a media library.
/// </summary>
/// <param name="MediaLibrary">The media library to be added.</param>
public class AddMediaLibraryRequest(MediaLibrary MediaLibrary) : IRequest<Result<MediaLibrary>>
{
}