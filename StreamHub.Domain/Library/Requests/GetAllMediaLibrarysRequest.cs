using MediatR;
using StreamHub.Common.Types;
using StreamHub.Domain.Library.Models;

namespace StreamHub.Domain.Library.Requests;

public class GetAllMediaLibrarysRequest : IRequest<Result<IEnumerable<MediaLibrary>>>
{
}