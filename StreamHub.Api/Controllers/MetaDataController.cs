using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        return Ok(await mediator.Send(new GetMetaDataProvidersRequest()));
    }
}