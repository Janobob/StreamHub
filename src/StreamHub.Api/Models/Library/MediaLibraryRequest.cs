using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StreamHub.Api.Models.Library;

public record MediaLibraryRequest(
    [property: Required]
    [property: JsonRequired]
    [property: Description("The unique identifier of the media library")]
    int Id,
    [property: Required]
    [property: Description("The name of the media library")]
    [property: MaxLength(200)]
    string Name,
    [property: Description("The description of the media library")]
    [property: MaxLength(1000)]
    string Description,
    [property: Required]
    [property: Description("The file path or directory in storage for the media library")]
    [property: MaxLength(1000)]
    string Path)
{
}