using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.Common;
using DDFilm.Contracts.Sessions.Responses;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.Sessions.Queries.Sessions
{
    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, Result<PaginatedList<SessionResponse>>>
    {
        public readonly ISessionReadService _sessionReadService;

        public GetSessionsQueryHandler(ISessionReadService sessionReadService)
        {
            _sessionReadService = sessionReadService;
        }

        public async Task<Result<PaginatedList<SessionResponse>>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        {
            var paginatedList = await _sessionReadService.GetAllSortedAsync(request.PaginationParams, cancellationToken);

            var result = paginatedList.Adapt<PaginatedList<SessionResponse>>();
            return Result.Ok(result);
        }
    }
}
