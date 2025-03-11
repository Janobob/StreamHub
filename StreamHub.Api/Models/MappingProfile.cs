using AutoMapper;
using StreamHub.Api.Models.Library;
using StreamHub.Api.Models.MetaData;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Api.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MetaDataProvider, MetaDataProviderResponse>();
        CreateMap<MetaDataSearchResult, MetaDataSearchResultResponse>();
        CreateMap<MediaLibrary, MediaLibraryResponse>();
        CreateMap<MediaLibraryRequest, MediaLibrary>();
    }
}