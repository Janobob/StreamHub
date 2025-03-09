namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents a media library that contains a collection of media items such as movies, series, and anime.
/// </summary>
public class MediaLibraryEntity
{
    /// <summary>
    ///     Gets or sets the unique identifier for the media library.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the media library. This field is required.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Gets or sets an optional description of the media library.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the storage path where the media library is located. This field is required.
    /// </summary>
    public required string Path { get; set; }

    /// <summary>
    ///     Gets or sets the collection of media items in the media library.
    /// </summary>
    public List<MediaEntity> MediaItems { get; set; } = [];
}