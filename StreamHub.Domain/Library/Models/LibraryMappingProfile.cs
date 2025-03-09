using AutoMapper;
using StreamHub.Persistence.Entities;

namespace StreamHub.Domain.Library.Models;

public class LibraryMappingProfile : Profile
{
    public LibraryMappingProfile()
    {
        CreateMap<MediaLibrary, MediaLibraryEntity>();
        CreateMap<MediaLibraryEntity, MediaLibrary>();
    }
}