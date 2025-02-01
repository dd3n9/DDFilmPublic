using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Contracts.SessionMovies.Responses;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.SessionMovies.Queries.GetSessionMovies
{
    public class GetSessionMoviesQueryHandler : IRequestHandler<GetSessionMoviesQuery, Result<IEnumerable<SessionMovieResponse>>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ISessionMovieReadService _sessionMovieReadService;

        public GetSessionMoviesQueryHandler(ISessionRepository sessionRepository,
            IApplicationUserRepository applicationUserRepository,
            ISessionMovieReadService sessionMovieReadService)
        {
            _sessionRepository = sessionRepository;
            _applicationUserRepository = applicationUserRepository;
            _sessionMovieReadService = sessionMovieReadService;
        }

        public async Task<Result<IEnumerable<SessionMovieResponse>>> Handle(GetSessionMoviesQuery request, CancellationToken cancellationToken)
        {
            var user = await _applicationUserRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);
            }

            var session = await _sessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if(session is null)
            {
                return Result.Fail(ApplicationErrors.Session.NotFound);
            }

            var sessionMovieDtos = await _sessionMovieReadService.GetMoviesBySessionIdAsync(request.SessionId, cancellationToken);
            return Result.Ok(sessionMovieDtos.Adapt<IEnumerable<SessionMovieResponse>>());
        }
    }
}
