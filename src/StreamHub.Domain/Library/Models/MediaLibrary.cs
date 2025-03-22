namespace StreamHub.Domain.Library.Models;

/// <summary>
///     Represents a media library.
/// </summary>
/// <param name="Id">The unique identifier for the media library.</param>
/// <param name="Name">The name of the media library.</param>
/// <param name="Description">The description of the media library.</param>
/// <param name="Path">The file path or directory in storage for the media library.</param>
public record MediaLibrary(int Id, string Name, string Description, string Path)
{
    // TODO: add media items to the media library
    ///// <summary>
    /////     Gets or sets the collection of media items in the media library.
    ///// </summary>
    // public List<MediaEntity> MediaItems { get; set; } = [];
}