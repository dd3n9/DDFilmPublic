using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.Common;
using DDFilm.Contracts.Sessions.Responses;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.Sessions.Queries.GetMySessions
{
    public class GetMySessionsQueryHandler : IRequestHandler<GetMySessionsQuery, Result<PaginatedList<SessionResponse>>>
    {
        private readonly ISessionReadService _sessionReadService;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public GetMySessionsQueryHandler(ISessionReadService sessionReadService,
            IApplicationUserRepository applicationUserRepository)
        {
            _sessionReadService = sessionReadService;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<Result<PaginatedList<SessionResponse>>> Handle(GetMySessionsQuery request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound); 

            var paginatedList = await _sessionReadService.GetByUserIdSortedAsync(request.PaginationParams, request.UserId, cancellationToken);

            var result = paginatedList.Adapt<PaginatedList<SessionResponse>>();
            return Result.Ok(result);
        }
    }
}
