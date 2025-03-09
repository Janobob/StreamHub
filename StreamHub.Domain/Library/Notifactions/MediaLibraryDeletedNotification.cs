using MediatR;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Notifactions;

public class MediaLibraryDeletedNotification(MediaLibrary MediaLibrary) : INotification
{
}