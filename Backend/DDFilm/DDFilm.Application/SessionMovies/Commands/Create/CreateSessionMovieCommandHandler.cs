using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Factories.SessionMovies;
using DDFilm.Domain.Repositories;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Commands.Create
{
    public class CreateSessionMovieCommandHandler : IRequestHandler<CreateSessionMovieCommand, Result>
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionMovieFactory _sessionMovieFactory;

        public CreateSessionMovieCommandHandler(IApplicationUserRepository userRepository,
            IMovieRepository movieRepository,
            ISessionRepository sessionRepository,
            ISessionMovieFactory sessionMovieFactory)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _sessionRepository = sessionRepository;
            _sessionMovieFactory = sessionMovieFactory;
        }

        public async Task<Result> Handle(CreateSessionMovieCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);
            }

            var session = await _sessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if(session is null)
            {
                return Result.Fail(ApplicationErrors.Session.NotFound);
            }

            var sessionMovie = await _sessionMovieFactory.CreateAsync(request.TmdbId, session.Id, request.MovieTitle, user.Id, cancellationToken);
            var result = session.AddMovie(sessionMovie);

            if(result.IsSuccess)
                await _sessionRepository.UpdateAsync(session, cancellationToken);

            return result;
        }
    }
}
