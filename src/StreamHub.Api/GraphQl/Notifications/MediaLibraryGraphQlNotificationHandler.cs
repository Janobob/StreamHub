using AutoMapper;
using HotChocolate.Subscriptions;
using MediatR;
using StreamHub.Domain.Library.Notifications;

namespace StreamHub.Api.GraphQl.Notifications;

public class MediaLibraryGraphQlNotificationHandler :
    INotificationHandler<MediaLibraryAddedNotification>,
    INotificationHandler<MediaLibraryDeletedNotification>,
    INotificationHandler<MediaLibraryUpdatedNotification>
{
    private readonly ITopicEventSender _eventSender;

    public MediaLibraryGraphQlNotificationHandler(ITopicEventSender eventSender, IMapper mapper)
    {
        _eventSender = eventSender;
    }

    public async Task Handle(MediaLibraryAddedNotification notification, CancellationToken cancellationToken)
    {
        await _eventSender.SendAsync("MediaLibrary_Added", notification.MediaLibrary, cancellationToken);
    }

    public async Task Handle(MediaLibraryDeletedNotification notification, CancellationToken cancellationToken)
    {
        await _eventSender.SendAsync("MediaLibrary_Deleted", notification.Id, cancellationToken);
    }

    public async Task Handle(MediaLibraryUpdatedNotification notification, CancellationToken cancellationToken)
    {
        await _eventSender.SendAsync("MediaLibrary_Updated", notification.MediaLibrary, cancellationToken);
    }
}