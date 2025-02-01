using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Commands.Delete
{
    public class DeleteSessionMovieCommandHandler : IRequestHandler<DeleteSessionMovieCommand, Result>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;

        public DeleteSessionMovieCommandHandler(ISessionRepository sessionRepository, 
            IApplicationUserRepository userRepository,
            IMovieRepository movieRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

        public async Task<Result> Handle(DeleteSessionMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByTmdbIdAsync(request.TmdbId, cancellationToken);
            if(movie is null)
            {
                return Result.Fail(ApplicationErrors.Movie.NotFound);                 
            }

            var session = await _sessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if(session is null)
            {
                return Result.Fail(ApplicationErrors.Session.NotFound);
            }

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if(user is null)
            {
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);
            }

            var result = session.RemoveMovie(movie.Id, user.Id);
            if(result.IsSuccess)
                await _sessionRepository.UpdateAsync(session, cancellationToken);

            return result;
        }
    }
}
