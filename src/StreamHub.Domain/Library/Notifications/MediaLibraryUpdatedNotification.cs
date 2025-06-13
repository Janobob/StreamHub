using MediatR;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Notifications;

/// <summary>
///     Notification for when a media library is updated.
/// </summary>
/// <param name="MediaLibrary">The media library that was updated.</param>
public record MediaLibraryUpdatedNotification(MediaLibrary MediaLibrary) : INotification
{
}