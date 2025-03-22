using MediatR;

namespace StreamHub.Domain.Library.Notifactions;

/// <summary>
///     Notification for when a media library is deleted.
/// </summary>
/// <param name="Id">The Id of the media library that was deleted.</param>
public record MediaLibraryDeletedNotification(int Id) : INotification
{
}