using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Services.Contracts;

namespace StreamHub.Domain.Library.Requests;

public class
    GetAllMediaLibrariesRequestHandler : IRequestHandler<GetAllMediaLibrariesRequest, Result<IEnumerable<MediaLibrary>>>
{
    private readonly IMediaLibraryService _mediaLibraryService;

    public GetAllMediaLibrariesRequestHandler(IMediaLibraryService mediaLibraryService)
    {
        _mediaLibraryService = mediaLibraryService;
    }

    public async Task<Result<IEnumerable<MediaLibrary>>> Handle(GetAllMediaLibrariesRequest request,
        CancellationToken cancellationToken)
    {
        return await _mediaLibraryService.GetMediaLibrariesAsync();
    }
}