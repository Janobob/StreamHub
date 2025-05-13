using Microsoft.AspNetCore.SignalR;
using StreamHub.Api.Models.Library;

namespace StreamHub.Api.Hubs.Notifiers;

public class LibraryHubNotifier : ILibraryHubNotifier
{
    private readonly IHubContext<LibraryHub> _hubContext;

    public LibraryHubNotifier(IHubContext<LibraryHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyAdded(MediaLibraryResponse dto, CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.All.SendAsync("mediaLibraryAdded", dto, cancellationToken);
    }

    public Task NotifyUpdated(MediaLibraryResponse dto, CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.All.SendAsync("mediaLibraryUpdated", dto, cancellationToken);
    }

    public Task NotifyDeleted(int id, CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.All.SendAsync("mediaLibraryDeleted", id, cancellationToken);
    }
}