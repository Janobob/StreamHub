using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Services.Contracts;

namespace StreamHub.Domain.Library.Requests;

public class GetMediaLibraryRequestHandler : IRequestHandler<GetMediaLibraryRequest, Result<MediaLibrary>>
{
    private readonly IMediaLibraryService _mediaLibraryService;

    public GetMediaLibraryRequestHandler(IMediaLibraryService mediaLibraryService)
    {
        _mediaLibraryService = mediaLibraryService;
    }

    public async Task<Result<MediaLibrary>> Handle(GetMediaLibraryRequest request, CancellationToken cancellationToken)
    {
        return await _mediaLibraryService.GetMediaLibraryAsync(request.Id);
    }
}