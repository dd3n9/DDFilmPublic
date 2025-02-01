using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.SessionMovies.Responses;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.SessionMovies.Queries.GetAllUnwatchedSessionMovies
{
    public class GetAllUnwatchedSessionMoviesQueryHandler : IRequestHandler<GetAllUnwatchedSessionMoviesQuery, Result<IEnumerable<SessionMovieResponse>>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISessionMovieReadService _sessionMovieReadService;
        private readonly ISessionParticipantReadService _sessionParticipantReadService;

        public GetAllUnwatchedSessionMoviesQueryHandler(ISessionRepository sessionRepository,
            IApplicationUserRepository applicationUserRepository,
            ISessionMovieReadService sessionMovieReadService,
            ISessionParticipantReadService sessionParticipantReadService)
        {
            _sessionRepository = sessionRepository;
            _applicationUserRepository = applicationUserRepository;
            _sessionMovieReadService = sessionMovieReadService;
            _sessionParticipantReadService = sessionParticipantReadService;
        }

        public async Task<Result<IEnumerable<SessionMovieResponse>>> Handle(GetAllUnwatchedSessionMoviesQuery request, CancellationToken cancellationToken)
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

            var watchedSessionMovies = await _sessionMovieReadService.GetUnwatchedMoviesBySessionIdAsync(request.SessionId, cancellationToken);

            return Result.Ok(watchedSessionMovies.Adapt<IEnumerable<SessionMovieResponse>>());
        }
    }
}
