using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StreamHub.Api.Models.Library;

public record MediaLibraryRequest
{
    [Required]
    [JsonRequired]
    [Description("The unique identifier of the media library")]
    public int Id { get; init; }

    [Required]
    [MaxLength(200)]
    [Description("The name of the media library")]
    public string Name { get; init; }

    [MaxLength(1000)]
    [Description("The description of the media library")]
    public string Description { get; init; }

    [Required]
    [MaxLength(1000)]
    [Description("The file path or directory in storage for the media library")]
    public string Path { get; init; }

    public MediaLibraryRequest(int id, string name, string description, string path)
    {
        Id = id;
        Name = name;
        Description = description;
        Path = path;
    }
}