using MediatR;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Notifactions;

/// <summary>
///     Notification for when a media library is added.
/// </summary>
/// <param name="MediaLibrary">The media library that was added.</param>
public record MediaLibraryAddedNotification(MediaLibrary MediaLibrary) : INotification
{
}