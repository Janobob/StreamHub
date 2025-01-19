namespace StreamHub.Persistence.Enums;

/// <summary>
///     Represents the status of a media entity.
/// </summary>
public enum MediaStatus
{
    /// <summary>
    ///     Indicates that the media is currently releasing new episodes or content.
    /// </summary>
    Continuing,

    /// <summary>
    ///     Indicates that the media has been released and is available for viewing.
    /// </summary>
    Released,

    /// <summary>
    ///     Indicates that the media has finished releasing all episodes or content.
    /// </summary>
    Ended,

    /// <summary>
    ///     Indicates that the media is scheduled to release in the future.
    /// </summary>
    Upcoming,

    /// <summary>
    ///     Indicates that the media's production or release has been canceled.
    /// </summary>
    Canceled,

    /// <summary>
    ///     Indicates that the media is temporarily paused or on a break.
    /// </summary>
    Hiatus,

    /// <summary>
    ///     Indicates that the status of the media is unknown or not specified.
    /// </summary>
    Unknown
}