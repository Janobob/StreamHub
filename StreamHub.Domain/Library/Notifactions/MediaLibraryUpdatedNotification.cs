using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Notifactions;

/// <summary>
///     Notification for when a media library is updated.
/// </summary>
/// <param name="MediaLibrary">The media library that was updated.</param>
public class MediaLibraryUpdatedNotification(MediaLibrary MediaLibrary)
{
}