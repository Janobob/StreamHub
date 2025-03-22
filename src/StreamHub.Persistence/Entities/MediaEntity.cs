using StreamHub.Common.Enums;

namespace StreamHub.Persistence.Entities;

/// <summary>
///     Represents a media item in the media library.
/// </summary>
/// <summary>
///     Represents the abstract base entity for all types of media (e.g., movies, series, anime).
/// </summary>
public abstract class MediaEntity
{
    /// <summary>
    ///     Gets or sets the unique identifier for the media entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier for the media entity from The tvdb Database
    /// </summary>
    public int TheTvdbId { get; set; }

    /// <summary>
    ///     Gets or sets the title of the media. This field is required.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    ///     Gets or sets the release date of the media.
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    ///     Gets or sets an optional description of the media.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets an optional studio of the media.
    /// </summary>
    public string? Studio { get; set; }

    /// <summary>
    ///     Gets or sets the storage path where the media file is located.
    /// </summary>
    public required string Path { get; set; }

    /// <summary>
    ///     Gets or sets the foreign key to the associated media library.
    /// </summary>
    public int MediaLibraryId { get; set; }

    /// <summary>
    ///     Gets or sets the navigation property to the media library that this media belongs to.
    /// </summary>
    public required MediaLibraryEntity MediaLibraryEntity { get; set; }

    /// <summary>
    ///     Gets or sets the type of media (e.g., Movie, Series, Anime).
    ///     This property is mapped to the discriminator column in the database.
    /// </summary>
    public virtual MediaType MediaType { get; init; }

    /// <summary>
    ///     Gets or sets the status of the media (e.g., continuing, ended).
    /// </summary>
    public MediaStatus Status { get; set; }
}