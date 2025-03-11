using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreamHub.Api.Extensions;
using StreamHub.Api.Models.Library;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Requests;

namespace StreamHub.Api.Controllers;

/// <summary>
///     Controller for managing media libraries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LibrariesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LibrariesController" /> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    /// <param name="mapper">The mapper instance for mapping models.</param>
    public LibrariesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    ///     Retrieves all media libraries.
    /// </summary>
    /// <returns>A list of <see cref="MediaLibraryResponse" /> representing all media libraries.</returns>
    [HttpGet]
    [ProducesResponseType<IEnumerable<MediaLibraryResponse>>(StatusCodes.Status200OK, "application/json")]
    [EndpointName(nameof(GetAllMediaLibrariesAsync))]
    [EndpointSummary("Get all media libraries")]
    [EndpointDescription("Retrieves all registered media libraries.")]
    public async Task<ActionResult<IEnumerable<MediaLibraryResponse>>> GetAllMediaLibrariesAsync()
    {
        var result = await _mediator.Send(new GetAllMediaLibrarysRequest());

        return result.MapList<MediaLibrary, MediaLibraryResponse>(_mapper)
            .ToActionResult(HttpContext, ProblemDetailsFactory);
    }

    /// <summary>
    ///     Retrieves a media library by its ID.
    /// </summary>
    /// <param name="id">The ID of the media library to retrieve.</param>
    /// <returns>A <see cref="MediaLibraryResponse" /> representing the media library.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType<MediaLibraryResponse>(StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [EndpointName(nameof(GetMediaLibraryAsync))]
    [EndpointSummary("Get media library by ID")]
    [EndpointDescription("Retrieves a specific media library using its unique identifier.")]
    public async Task<ActionResult<MediaLibraryResponse>> GetMediaLibraryAsync(
        [Required] [FromRoute] [Description("The ID of the media library")]
        int id
    )
    {
        var result = await _mediator.Send(new GetMediaLibraryRequest(id));

        return result.Map<MediaLibrary, MediaLibraryResponse>(_mapper)
            .ToActionResult(HttpContext, ProblemDetailsFactory);
    }

    /// <summary>
    ///     Adds a new media library.
    /// </summary>
    /// <param name="mediaLibraryRequest">The media library data to add.</param>
    /// <returns>The newly created <see cref="MediaLibraryResponse" />.</returns>
    [HttpPost]
    [ProducesResponseType<MediaLibraryResponse>(StatusCodes.Status201Created, "application/json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [EndpointName(nameof(AddMediaLibraryAsync))]
    [EndpointSummary("Add a new media library")]
    [EndpointDescription("Creates a new media library and stores it in the system.")]
    public async Task<ActionResult<MediaLibraryResponse>> AddMediaLibraryAsync(
        [Required] [FromBody] [Description("The media library to add")]
        MediaLibraryRequest mediaLibraryRequest
    )
    {
        var mediaLibrary = _mapper.Map<MediaLibrary>(mediaLibraryRequest);
        var result = await _mediator.Send(new AddMediaLibraryRequest(mediaLibrary));

        return result.Map<MediaLibrary, MediaLibraryResponse>(_mapper)
            .ToActionResult(HttpContext, ProblemDetailsFactory);
    }

    /// <summary>
    ///     Updates an existing media library.
    /// </summary>
    /// <param name="id">The ID of the media library to update.</param>
    /// <param name="library">The updated media library data.</param>
    /// <returns>The updated <see cref="MediaLibraryResponse" />.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType<MediaLibraryResponse>(StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [EndpointName(nameof(UpdateLibraryAsync))]
    [EndpointSummary("Update media library")]
    [EndpointDescription("Modifies an existing media library using the provided data.")]
    public async Task<ActionResult<MediaLibraryResponse>> UpdateLibraryAsync(
        [Required] [FromRoute] [Description("The ID of the media library")]
        int id,
        [Required] [FromBody] [Description("The media library to update")]
        MediaLibraryRequest library)
    {
        var mediaLibrary = _mapper.Map<MediaLibrary>(library);

        if (mediaLibrary.Id != id)
        {
            return BadRequest("The ID in the request body does not match the ID in the URL.");
        }

        var result = await _mediator.Send(new UpdateMediaLibraryRequest(mediaLibrary));
        return result.Map<MediaLibrary, MediaLibraryResponse>(_mapper)
            .ToActionResult(HttpContext, ProblemDetailsFactory);
    }


    /// <summary>
    ///     Deletes a media library by its ID.
    /// </summary>
    /// <param name="id">The ID of the media library to delete.</param>
    /// <returns>No content on success.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [EndpointName(nameof(DeleteLibraryAsync))]
    [EndpointSummary("Delete media library")]
    [EndpointDescription("Removes a media library from the system by its unique identifier.")]
    public async Task<IActionResult> DeleteLibraryAsync(
        [Required] [FromRoute] [Description("The ID of the media library")]
        int id)
    {
        // TODO: Rewrite delete method to use only the ID
        var mediaLibrary = new MediaLibrary(id, "", "", "");
        var result = await _mediator.Send(new DeleteMediaLibraryRequest(mediaLibrary));

        // TODO: Implement proper handling of the result
        // return result.ToActionResult(HttpContext, ProblemDetailsFactory); but for IActionResult
        return NoContent();
    }
}