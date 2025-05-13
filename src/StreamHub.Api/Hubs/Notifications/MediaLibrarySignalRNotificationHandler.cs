using AutoMapper;
using MediatR;
using StreamHub.Api.Hubs.Notifiers;
using StreamHub.Api.Models.Library;
using StreamHub.Domain.Library.Notifications;

namespace StreamHub.Api.Hubs.Notifications;

public class MediaLibrarySignalRNotificationHandler :
    INotificationHandler<MediaLibraryAddedNotification>,
    INotificationHandler<MediaLibraryDeletedNotification>,
    INotificationHandler<MediaLibraryUpdatedNotification>
{
    private readonly IMapper _mapper;
    private readonly ILibraryHubNotifier _notifier;

    public MediaLibrarySignalRNotificationHandler(ILibraryHubNotifier notifier, IMapper mapper)
    {
        _notifier = notifier;
        _mapper = mapper;
    }

    public async Task Handle(MediaLibraryAddedNotification notification, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<MediaLibraryResponse>(notification.MediaLibrary);
        await _notifier.NotifyAdded(dto, cancellationToken);
    }

    public async Task Handle(MediaLibraryDeletedNotification notification, CancellationToken cancellationToken)
    {
        await _notifier.NotifyDeleted(notification.Id, cancellationToken);
    }

    public async Task Handle(MediaLibraryUpdatedNotification notification, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<MediaLibraryResponse>(notification.MediaLibrary);
        await _notifier.NotifyUpdated(dto, cancellationToken);
    }
}