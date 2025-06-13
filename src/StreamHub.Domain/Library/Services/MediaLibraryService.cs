using AutoMapper;
using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Notifications;
using StreamHub.Domain.Library.Services.Contracts;
using StreamHub.Persistence.Entities;
using StreamHub.Persistence.Repositories.Contracts;

namespace StreamHub.Domain.Library.Services;

/// <summary>
///     Implementation of the <see cref="IMediaLibraryService" /> interface.
///     Provides methods for interacting with media libraries.
/// </summary>
public class MediaLibraryService : IMediaLibraryService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IMediaLibraryRepository _repo;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MediaLibraryService" /> class.
    /// </summary>
    /// <param name="repo">The media library repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="mediator">The MediatR instance.</param>
    public MediaLibraryService(IMediaLibraryRepository repo, IMapper mapper, IMediator mediator)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<Result<MediaLibrary>> GetMediaLibraryAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);

        return entity == null
            ? Result<MediaLibrary>.Failure(new ArgumentException($"MediaLibrary with id {id} not found"))
            : Result<MediaLibrary>.Success(_mapper.Map<MediaLibrary>(entity));
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<MediaLibrary>>> GetMediaLibrariesAsync()
    {
        var entities = await _repo.GetAllAsync();

        return Result<IEnumerable<MediaLibrary>>.Success(_mapper.Map<IEnumerable<MediaLibrary>>(entities));
    }

    /// <inheritdoc />
    public async Task<Result<MediaLibrary>> AddMediaLibraryAsync(MediaLibrary mediaLibrary)
    {
        var entity = await _repo.AddAsync(_mapper.Map<MediaLibraryEntity>(mediaLibrary));
        await _repo.SaveChangesAsync();

        await _mediator.Publish(new MediaLibraryAddedNotification(_mapper.Map<MediaLibrary>(entity)));

        return Result<MediaLibrary>.Success(_mapper.Map<MediaLibrary>(entity));
    }

    /// <inheritdoc />
    public async Task<Result<MediaLibrary>> UpdateMediaLibraryAsync(MediaLibrary mediaLibrary)
    {
        _repo.Update(_mapper.Map<MediaLibraryEntity>(mediaLibrary));
        await _repo.SaveChangesAsync();

        await _mediator.Publish(new MediaLibraryUpdatedNotification(mediaLibrary));

        return Result<MediaLibrary>.Success(mediaLibrary);
    }

    /// <inheritdoc />
    public async Task<Result<bool>> DeleteMediaLibraryAsync(int id)
    {
        var deleted = await _repo.DeleteAsync(id);

        if (!deleted)
        {
            return Result<bool>.Failure(new ArgumentException($"MediaLibrary with id {id} not found"));
        }

        await _repo.SaveChangesAsync();
        await _mediator.Publish(new MediaLibraryDeletedNotification(id));

        return Result<bool>.Success(true);
    }
}