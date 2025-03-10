using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StreamHub.Api.Models.Library;

/// <summary>
///     Response model for a media library.
/// </summary>
/// <param name="Id">The unique identifier for the media library.</param>
/// <param name="Name">The name of the media library.</param>
/// <param name="Description">The description of the media library.</param>
/// <param name="Path">The file path or directory in storage for the media library.</param>
public record MediaLibraryResponse(
    [property: Required]
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