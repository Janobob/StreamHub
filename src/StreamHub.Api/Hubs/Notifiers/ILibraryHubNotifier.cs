using StreamHub.Api.Models.Library;

namespace StreamHub.Api.Hubs.Notifiers;

public interface ILibraryHubNotifier
{
    Task NotifyAdded(MediaLibraryResponse dto, CancellationToken cancellationToken = default);
    Task NotifyUpdated(MediaLibraryResponse dto, CancellationToken cancellationToken = default);
    Task NotifyDeleted(int id, CancellationToken cancellationToken = default);
}