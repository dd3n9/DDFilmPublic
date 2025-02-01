using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.SessionParticipant.Responses;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.SessionParticipants.Queries.GetSessionParticpants
{
    public class GetSessionParticipantsQueryHandler : IRequestHandler<GetSessionParticipantsQuery, Result<IEnumerable<SessionParticipantResponse>>>
    {
        private readonly ISessionParticipantReadService _sessionParticipantReadService;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISessionRepository _sessionRepository;

        public GetSessionParticipantsQueryHandler(ISessionParticipantReadService sessionParticipantReadService,
            IApplicationUserRepository applicationUserRepository,
            ISessionRepository sessionRepository)
        {
            _sessionParticipantReadService = sessionParticipantReadService;
            _applicationUserRepository = applicationUserRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<IEnumerable<SessionParticipantResponse>>> Handle(GetSessionParticipantsQuery request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);
            }

            var session = await _sessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if (session is null)
            {
                return Result.Fail(ApplicationErrors.Session.NotFound);
            }

            var isUserParticipant = await _sessionParticipantReadService.IsUserParticipant(session.Id, user.Id, cancellationToken);
            if (!isUserParticipant)
            {
                return Result.Fail(ApplicationErrors.SessionParticipant.NotFound);
            }

            var sessionParticipantDtos = await _sessionParticipantReadService.GetBySessionIdAsync(session.Id, cancellationToken);

            return Result.Ok(sessionParticipantDtos.Adapt<IEnumerable<SessionParticipantResponse>>());
        }
    }
}
