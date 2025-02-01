using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.Common;
using DDFilm.Contracts.SessionMovies.Responses;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.SessionMovies.Queries.GetUnwatchedSessionMovies
{
    public class GetUnwatchedSessionMoviesQueryHandler : IRequestHandler<GetUnwatchedSessionMoviesQuery, Result<PaginatedList<SessionMovieResponse>>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISessionMovieReadService _sessionMovieReadService;
        private readonly ISessionParticipantReadService _sessionParticipantReadService;

        public GetUnwatchedSessionMoviesQueryHandler(ISessionRepository sessionRepository,
            IApplicationUserRepository applicationUserRepository,
            ISessionMovieReadService sessionMovieReadService,
            ISessionParticipantReadService sessionParticipantReadService)
        {
            _sessionRepository = sessionRepository;
            _applicationUserRepository = applicationUserRepository;
            _sessionMovieReadService = sessionMovieReadService;
            _sessionParticipantReadService = sessionParticipantReadService;
        }

        public async Task<Result<PaginatedList<SessionMovieResponse>>> Handle(GetUnwatchedSessionMoviesQuery request, CancellationToken cancellationToken)
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

            var watchedSessionMovies = await _sessionMovieReadService.GetUnwatchedMoviesBySessionIdAsync(request.PaginationParams, request.SessionId, cancellationToken);

            return Result.Ok(watchedSessionMovies.Adapt<PaginatedList<SessionMovieResponse>>());
        }
    }
}
