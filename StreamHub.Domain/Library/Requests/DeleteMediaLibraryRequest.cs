using MediatR;
using StreamHub.Common.Types;

namespace StreamHub.Domain.Library.Requests;

/// <summary>
///     Request to delete a media library.
/// </summary>
/// <param name="Id">The ID of the media library to be deleted.</param>
public record DeleteMediaLibraryRequest(int Id) : IRequest<Result<bool>>
{
}