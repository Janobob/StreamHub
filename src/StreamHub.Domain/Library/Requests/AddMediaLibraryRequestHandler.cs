using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Services.Contracts;

namespace StreamHub.Domain.Library.Requests;

public class AddMediaLibraryRequestHandler : IRequestHandler<AddMediaLibraryRequest, Result<MediaLibrary>>
{
    private readonly IMediaLibraryService _mediaLibraryService;

    public AddMediaLibraryRequestHandler(IMediaLibraryService mediaLibraryService)
    {
        _mediaLibraryService = mediaLibraryService;
    }

    public async Task<Result<MediaLibrary>> Handle(AddMediaLibraryRequest request, CancellationToken cancellationToken)
    {
        return await _mediaLibraryService.AddMediaLibraryAsync(request.MediaLibrary);
    }
}