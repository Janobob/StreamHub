﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreamHub.Api.Extensions;
using StreamHub.Api.Models.MetaData;
using StreamHub.Common.Enums;
using StreamHub.Domain.MetaData.Models;
using StreamHub.Domain.MetaData.Requests;

namespace StreamHub.Api.Controllers;

/// <summary>
///     Controller for handling metadata-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MetaDataController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MetaDataController" /> class.
    /// </summary>
    /// <param name="mediator">The mediator instance for handling requests.</param>
    /// <param name="mapper">The mapper instance for mapping models.</param>
    public MetaDataController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    ///     Retrieves all registered and available metadata providers.
    /// </summary>
    /// <returns>
    ///     A list of <see cref="MetaDataProvider" /> representing the metadata providers.
    /// </returns>
    [HttpGet("providers")]
    [EndpointName(nameof(GetMetaDataProvidersAsync))]
    [EndpointSummary("Get all metadata providers")]
    [EndpointDescription("Get all registered and available metadata providers.")]
    [ProducesResponseType<IEnumerable<MetaDataProviderResponse>>(StatusCodes.Status200OK, "application/json")]
    public async Task<ActionResult<IEnumerable<MetaDataProviderResponse>>> GetMetaDataProvidersAsync()
    {
        var result = await _mediator.Send(new GetAllMetaDataProvidersRequest());

        return result.MapList<MetaDataProvider, MetaDataProviderResponse>(_mapper)
            .ToActionResult(HttpContext, ProblemDetailsFactory);
    }

    /// <summary>
    ///     Retrieves a specific metadata provider by name.
    /// </summary>
    /// <param name="name">The name of the metadata provider to retrieve.</param>
    /// <returns>A <see cref="MetaDataProvider" /> representing the metadata provider.</returns>
    [HttpGet("providers/{name}")]
    [EndpointName(nameof(GetMetaDataProviderAsync))]
    [EndpointSummary("Get metadata provider by name")]
    [EndpointDescription("Get a specific metadata provider by name.")]
    [ProducesResponseType<MetaDataProviderResponse>(StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<MetaDataProviderResponse>> GetMetaDataProviderAsync(
        [Required]
        [FromRoute]
        [Description("The name of the metadata provider")]
        [System.ComponentModel.DefaultValue("Tvdb")]
        string name)
    {
        var result = await _mediator.Send(new GetMetaDataProviderRequest(name));

        return result.Map<MetaDataProvider, MetaDataProviderResponse>(_mapper)
            .ToOkActionResult(HttpContext, ProblemDetailsFactory);
    }

    /// <summary>
    ///     Searches for media metadata items from metadata providers based on the provided query.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <param name="name">The name of the metadata provider to search within (optional).</param>
    /// <param name="type">The type of media to search for (e.g., Movie, Series). Default is <see cref="MediaType.All" />.</param>
    /// <param name="limit">The maximum number of search results to return. Default is 10.</param>
    /// <returns>
    ///     An
    ///     <see cref="IEnumerable{T}" /> of <see cref="MetaDataSearchResultResponse" /> representing the search results.
    /// </returns>
    [HttpGet("search")]
    [EndpointName(nameof(SearchMediaMetaData))]
    [EndpointSummary("Search for media from metadata providers")]
    [EndpointDescription("Searches for movies, series, or media items across one or all metadata providers.")]
    [ProducesResponseType<IEnumerable<MetaDataSearchResultResponse>>(StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IEnumerable<MetaDataSearchResultResponse>>> SearchMediaMetaData(
        [Required] [FromQuery] [Description("The search query string")]
        string query,
        [FromQuery]
        [Description("The name of the metadata provider to search within")]
        [System.ComponentModel.DefaultValue("Tvdb")]
        string? name,
        [FromQuery] [Description("The type of media to search for (e.g., Movie, Series)")]
        MediaType type = MediaType.All,
        [FromQuery] [Description("The maximum number of search results to return")]
        int limit = 10
    )
    {
        var result = await _mediator.Send(new SearchMediaMetaDataRequest(query, name, type, limit));

        return result.MapList<MetaDataSearchResult, MetaDataSearchResultResponse>(_mapper)
            .ToOkActionResult(HttpContext, ProblemDetailsFactory);
    }
}