using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Services.Contracts;

namespace StreamHub.Domain.Library.Requests;

public class UpdateMediaLibraryRequestHandler : IRequestHandler<UpdateMediaLibraryRequest, Result<MediaLibrary>>
{
    private readonly IMediaLibraryService _mediaLibraryService;

    public UpdateMediaLibraryRequestHandler(IMediaLibraryService mediaLibraryService)
    {
        _mediaLibraryService = mediaLibraryService;
    }

    public async Task<Result<MediaLibrary>> Handle(UpdateMediaLibraryRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediaLibraryService.UpdateMediaLibraryAsync(request.MediaLibrary);
    }
}