using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

public class GetMediaLibraryRequest : IRequest<Result<MediaLibrary>>
{
}