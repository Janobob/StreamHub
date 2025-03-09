using AutoMapper;
using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Notifactions;
using StreamHub.Domain.Library.Services.Contracts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Domain.Library.Services;

public class MediaLibraryService : IMediaLibraryService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IMediaLibraryRepository _repo;

    public MediaLibraryService(IMediaLibraryRepository repo, IMapper mapper, IMediator mediator)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Result<MediaLibrary>> GetMediaLibraryAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);

        return entity == null
            ? Result<MediaLibrary>.Failure(new ArgumentException($"MediaLibrary with id {id} not found"))
            : Result<MediaLibrary>.Success(_mapper.Map<MediaLibrary>(entity));
    }

    public async Task<Result<IEnumerable<MediaLibrary>>> GetMediaLibrariesAsync()
    {
        var entities = await _repo.GetAllAsync();

        return Result<IEnumerable<MediaLibrary>>.Success(_mapper.Map<IEnumerable<MediaLibrary>>(entities));
    }

    public async Task<Result<MediaLibrary>> AddMediaLibraryAsync(MediaLibrary mediaLibrary)
    {
        await _repo.AddAsync(_mapper.Map<MediaLibraryEntity>(mediaLibrary));
        await _repo.SaveChangesAsync();

        await _mediator.Publish(new MediaLibraryAddedNotification(mediaLibrary));

        return Result<MediaLibrary>.Success(mediaLibrary);
    }

    public async Task<Result<MediaLibrary>> UpdateMediaLibraryAsync(MediaLibrary mediaLibrary)
    {
        _repo.Update(_mapper.Map<MediaLibraryEntity>(mediaLibrary));
        await _repo.SaveChangesAsync();

        await _mediator.Publish(new MediaLibraryUpdatedNotification(mediaLibrary));

        return Result<MediaLibrary>.Success(mediaLibrary);
    }

    public Task<Result<MediaLibrary>> DeleteMediaLibraryAsync(int id)
    {
        throw new NotImplementedException();
    }
}