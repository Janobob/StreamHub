using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Services.Contracts;

/// <summary>
///     Interface for Media Library Service.
///     Provides methods to manage media libraries.
/// </summary>
public interface IMediaLibraryService
{
    /// <summary>
    ///     Gets a media library by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the media library.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the media library.</returns>
    Task<Result<MediaLibrary>> GetMediaLibraryAsync(int id);

    /// <summary>
    ///     Gets all media libraries.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of media libraries.</returns>
    Task<Result<IEnumerable<MediaLibrary>>> GetMediaLibrariesAsync();

    /// <summary>
    ///     Adds a new media library.
    /// </summary>
    /// <param name="mediaLibrary">The media library to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added media library.</returns>
    Task<Result<MediaLibrary>> AddMediaLibraryAsync(MediaLibrary mediaLibrary);

    /// <summary>
    ///     Updates an existing media library.
    /// </summary>
    /// <param name="mediaLibrary">The media library to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated media library.</returns>
    Task<Result<MediaLibrary>> UpdateMediaLibraryAsync(MediaLibrary mediaLibrary);

    /// <summary>
    ///     Deletes a media library.
    /// </summary>
    /// <param name="mediaLibrary">The media library to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deleted media library.</returns>
    Task<Result<MediaLibrary>> DeleteMediaLibraryAsync(MediaLibrary mediaLibrary);
}