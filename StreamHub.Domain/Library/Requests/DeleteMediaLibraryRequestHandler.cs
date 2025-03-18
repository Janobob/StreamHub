using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Services.Contracts;

namespace StreamHub.Domain.Library.Requests;

public class DeleteMediaLibraryRequestHandler : IRequestHandler<DeleteMediaLibraryRequest, Result<bool>>
{
    private readonly IMediaLibraryService _mediaLibraryService;

    public DeleteMediaLibraryRequestHandler(IMediaLibraryService mediaLibraryService)
    {
        _mediaLibraryService = mediaLibraryService;
    }

    public async Task<Result<bool>> Handle(DeleteMediaLibraryRequest request, CancellationToken cancellationToken)
    {
        return await _mediaLibraryService.DeleteMediaLibraryAsync(request.Id);
    }
}