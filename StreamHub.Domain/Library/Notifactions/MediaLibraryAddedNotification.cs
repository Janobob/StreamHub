using MediatR;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Notifactions;

public class MediaLibraryAddedNotification(MediaLibrary MediaLibrary) : INotification
{
}