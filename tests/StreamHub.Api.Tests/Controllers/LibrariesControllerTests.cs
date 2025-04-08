using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;
using StreamHub.Api.Controllers;
using StreamHub.Api.Models.Library;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;
using StreamHub.Domain.Library.Requests;

namespace StreamHub.Api.Tests.Controllers;

public class LibrariesControllerTests
{
    private readonly LibrariesController _controller;
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    public LibrariesControllerTests()
    {
        var problemDetailsFactory = Substitute.For<ProblemDetailsFactory>();

        problemDetailsFactory
            .CreateProblemDetails(
                Arg.Any<HttpContext>(),
                Arg.Any<int?>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
            .Returns(ci => new ProblemDetails
            {
                Status = ci.ArgAt<int?>(1) ?? 500,
                Title = "Mocked Problem",
                Detail = "This is a test problem detail"
            });

        problemDetailsFactory
            .CreateValidationProblemDetails(
                Arg.Any<HttpContext>(),
                Arg.Any<ModelStateDictionary>(),
                Arg.Any<int?>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
            .Returns(ci => new ValidationProblemDetails(ci.ArgAt<ModelStateDictionary>(1))
            {
                Status = ci.ArgAt<int?>(2) ?? 400,
                Title = "Mocked Validation Problem"
            });

        _controller = new LibrariesController(_mediator, _mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            },
            ProblemDetailsFactory = problemDetailsFactory
        };
    }

    [Fact]
    public async Task AddMediaLibraryAsync_ShouldReturnCreatedResult()
    {
        // Arrange
        var request = new MediaLibraryRequest(0, "Test Library", "Test Description", "Test Path");
        var mapped = new MediaLibrary(1, "Test Library", "Test Description", "Test Path");
        var response = new MediaLibraryResponse(1, "Test Library", "Test Description", "Test Path");

        _mapper.Map<MediaLibrary>(request).Returns(mapped);
        _mediator
            .Send(Arg.Is<AddMediaLibraryRequest>(r => r.MediaLibrary == mapped), Arg.Any<CancellationToken>())
            .Returns(Result<MediaLibrary>.Success(mapped));
        _mapper.Map<MediaLibraryResponse>(mapped).Returns(response);

        // Act
        var result = await _controller.AddMediaLibraryAsync(request);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        Assert.Equal(response, createdResult.Value);
    }

    //TODO: Add test for AddMediaLibraryAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid

    [Fact]
    public async Task DeleteLibraryAsync_ShouldReturnNoContent()
    {
        // Arrange
        _mediator
            .Send(Arg.Is<DeleteMediaLibraryRequest>(r => r.Id == 1), Arg.Any<CancellationToken>())
            .Returns(Result<bool>.Success(true));

        // Act
        var result = await _controller.DeleteLibraryAsync(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteLibraryAsync_ShouldReturnNotFound()
    {
        // Arrange
        _mediator
            .Send(Arg.Is<DeleteMediaLibraryRequest>(r => r.Id == 1), Arg.Any<CancellationToken>())
            .Returns(Result<bool>.Failure(new ArgumentException("MediaLibrary not found")));

        // Act
        var result = await _controller.DeleteLibraryAsync(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetAllMediaLibrariesAsync_ShouldReturnOkResult()
    {
        // Arrange
        var libraries = new List<MediaLibrary>();
        var mapped = new List<MediaLibraryResponse>();
        _mediator
            .Send(Arg.Is<GetAllMediaLibrariesRequest>(_ => true), Arg.Any<CancellationToken>())
            .Returns(Result<IEnumerable<MediaLibrary>>.Success(libraries));
        _mapper.Map<List<MediaLibraryResponse>>(libraries).Returns(mapped);

        // Act
        var result = await _controller.GetAllMediaLibrariesAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(mapped, okResult.Value);
    }

    [Fact]
    public async Task GetMediaLibraryAsync_ShouldReturnNotFound_WhenLibraryDoesNotExist()
    {
        // Arrange
        var exception = new ArgumentException("Library not found");

        _mediator
            .Send(Arg.Is<GetMediaLibraryRequest>(r => r.Id == 99), Arg.Any<CancellationToken>())
            .Returns(Result<MediaLibrary>.Failure(exception));

        // Act
        var result = await _controller.GetMediaLibraryAsync(99);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetMediaLibraryAsync_ShouldReturnOk_WhenLibraryExists()
    {
        // Arrange
        var lib = new MediaLibrary(1, "Test Library", "Test Description", "Test Path");
        var mapped = new MediaLibraryResponse(1, "Test Library", "Test Description", "Test Path");

        _mediator
            .Send(Arg.Is<GetMediaLibraryRequest>(r => r.Id == 1), Arg.Any<CancellationToken>())
            .Returns(Result<MediaLibrary>.Success(lib));
        _mapper.Map<MediaLibraryResponse>(lib).Returns(mapped);

        // Act
        var result = await _controller.GetMediaLibraryAsync(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(mapped, okResult.Value);
    }

    [Fact]
    public async Task UpdateLibraryAsync_ShouldReturnBadRequest_WhenIdMismatch()
    {
        // Arrange
        var request = new MediaLibraryRequest(2, "Test Library", "Test Description", "Test Path");
        _mapper.Map<MediaLibrary>(request)
            .Returns(new MediaLibrary(2, "Test Library", "Test Description", "Test Path"));

        // Act
        var result = await _controller.UpdateLibraryAsync(1, request);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
    }

    [Fact]
    public async Task UpdateLibraryAsync_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var request = new MediaLibraryRequest(1, "Updated", "Updated Description", "Updated Path");
        var mapped = new MediaLibrary(1, "Updated", "Updated Description", "Updated Path");
        var response = new MediaLibraryResponse(1, "Updated", "Updated Description", "Updated Path");

        _mapper.Map<MediaLibrary>(request).Returns(mapped);
        _mediator
            .Send(Arg.Is<UpdateMediaLibraryRequest>(r => r.MediaLibrary == mapped), Arg.Any<CancellationToken>())
            .Returns(Result<MediaLibrary>.Success(mapped));
        _mapper.Map<MediaLibraryResponse>(mapped).Returns(response);

        // Act
        var result = await _controller.UpdateLibraryAsync(1, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(response, okResult.Value);
    }

    // TODO: Add test for UpdateLibraryAsync_ShouldReturnNotFound_WhenLibraryDoesNotExist
    // TODO: Add test for UpdateLibraryAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid
}