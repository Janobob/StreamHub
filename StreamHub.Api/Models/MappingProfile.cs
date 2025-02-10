using AutoMapper;
using StreamHub.Api.Models.MetaData;
using StreamHub.Domain.MetaData.Models;

namespace StreamHub.Api.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MetaDataProvider, MetaDataProviderResponse>();
        CreateMap<MetaDataSearchResult, MetaDataSearchResultResponse>();
    }
}