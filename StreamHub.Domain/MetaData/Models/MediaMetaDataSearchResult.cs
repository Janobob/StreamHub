using StreamHub.Persistence.Enums;

namespace StreamHub.Domain.MetaData.Models;

public class MediaMetaDataSearchResult
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required MediaType MediaType { get; set; }
}