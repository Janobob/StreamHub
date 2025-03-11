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
    string Name,
    [property: Description("The description of the media library")]
    string Description,
    [property: Required]
    [property: Description("The file path or directory in storage for the media library")]
    string Path)
{
}