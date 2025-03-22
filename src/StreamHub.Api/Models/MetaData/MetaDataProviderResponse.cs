using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StreamHub.Api.Models.MetaData;

/// <summary>
///     Response model for a metadata provider.
/// </summary>
/// <param name="Name">The name of the metadata provider.</param>
public record MetaDataProviderResponse(
    [property: Required]
    [property: Description("The name of the metadata provider")]
    string Name
)
{
}