using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.Sessions.Responses;
using DDFilm.Domain.Common.Errors;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.Sessions.Queries.GetByName
{
    public class GetSessionByNameQueryHandler : IRequestHandler<GetSessionByNameQuery, Result<SessionResponse>>
    {
        private readonly ISessionReadService _sessionReadService;

        public GetSessionByNameQueryHandler(ISessionReadService sessionReadService)
        {
            _sessionReadService = sessionReadService;
        }

        public async Task<Result<SessionResponse>> Handle(GetSessionByNameQuery request, CancellationToken cancellationToken)
        {
            var sessionDto = await _sessionReadService.GetByNameAsync(request.SessionName, cancellationToken);
            if (sessionDto == null)
            {
                return Result.Fail(ApplicationErrors.Session.NotFound);
            }

            return Result.Ok(sessionDto.Adapt<SessionResponse>());
        }
    }
}
