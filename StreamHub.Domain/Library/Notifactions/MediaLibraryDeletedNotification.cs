using MediatR;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Notifactions;

/// <summary>
///     Notification for when a media library is deleted.
/// </summary>
/// <param name="MediaLibrary">The media library that was deleted.</param>
public class MediaLibraryDeletedNotification(MediaLibrary MediaLibrary) : INotification
{
}