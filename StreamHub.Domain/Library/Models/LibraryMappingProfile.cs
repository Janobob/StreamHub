using AutoMapper;
using StreamHub.Persistence.Entities;

namespace StreamHub.Domain.Library.Models;

/// <summary>
///     Profile class for mapping between MediaLibrary and MediaLibraryEntity.
/// </summary>
public class LibraryMappingProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LibraryMappingProfile" /> class.
    ///     Configures the mappings between MediaLibrary and MediaLibraryEntity.
    /// </summary>
    public LibraryMappingProfile()
    {
        CreateMap<MediaLibrary, MediaLibraryEntity>();
        CreateMap<MediaLibraryEntity, MediaLibrary>();
    }
}