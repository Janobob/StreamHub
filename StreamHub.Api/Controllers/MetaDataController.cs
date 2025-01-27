using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreamHub.Api.Extensions;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Requests;

namespace StreamHub.Api.Controllers;

/// <summary>
///     Controller for handling metadata-related operations.
/// </summary>
/// <param name="mediator">The mediator instance for handling requests.</param>
[ApiController]
[Route("api/[controller]")]
public class MetaDataController(
    IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Retrieves all registered and available metadata providers.
    /// </summary>
    /// <returns>
    ///     A list of <see cref="MetaDataProvider" /> representing the metadata providers.
    /// </returns>
    [HttpGet("providers")]
    [EndpointName(nameof(GetMetaDataProviders))]
    [EndpointSummary("Get all metadata providers")]
    [EndpointDescription("Get all registered and available metadata providers.")]
    [ProducesResponseType<IEnumerable<MetaDataProvider>>(StatusCodes.Status200OK, "application/json")]
    public async Task<ActionResult<IEnumerable<MetaDataProvider>>> GetMetaDataProviders()
    {
        var result = await mediator.Send(new GetMetaDataProvidersRequest());

        return result.ToActionResult(HttpContext, ProblemDetailsFactory);
    }

    /// <summary>
    ///     Retrieves a specific metadata provider by name.
    /// </summary>
    /// <param name="name">The name of the metadata provider to retrieve.</param>
    /// <returns>A <see cref="MetaDataProvider" /> representing the metadata provider.</returns>
    [HttpGet("providers/{name}")]
    [EndpointName(nameof(GetMetaDataProvider))]
    [EndpointSummary("Get metadata provider by name")]
    [EndpointDescription("Get a specific metadata provider by name.")]
    [ProducesResponseType<MetaDataProvider>(StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<MetaDataProvider>> GetMetaDataProvider(
        [Description("The name of the metadata provider")] [Required] [System.ComponentModel.DefaultValue("Tvdb")]
        string name)
    {
        // TODO: change to use the response model with the description for openapi
        var result = await mediator.Send(new GetMetaDataProviderRequest(name));

        return result.ToActionResult(HttpContext, ProblemDetailsFactory);
    }
}