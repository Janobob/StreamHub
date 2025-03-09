using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Services.Contracts;

public interface IMediaLibraryService
{
    Task<Result<MediaLibrary>> GetMediaLibraryAsync(int id);

    Task<Result<IEnumerable<MediaLibrary>>> GetMediaLibrariesAsync();

    Task<Result<MediaLibrary>> AddMediaLibraryAsync(MediaLibrary mediaLibrary);

    Task<Result<MediaLibrary>> UpdateMediaLibraryAsync(MediaLibrary mediaLibrary);

    Task<Result<MediaLibrary>> DeleteMediaLibraryAsync(int id);
}