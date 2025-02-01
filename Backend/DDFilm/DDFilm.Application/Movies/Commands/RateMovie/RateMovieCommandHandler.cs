using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.Services;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Movies.Commands.RateMovie
{
    public class RateMovieCommandHandler : IRequestHandler<RateMovieCommand, Result>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly MovieDomainService _movieDomainService;

        public RateMovieCommandHandler(IMovieRepository movieRepository,
            IApplicationUserRepository userRepository,
            MovieDomainService movieDomainService)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _movieDomainService = movieDomainService;
        }

        public async Task<Result> Handle(RateMovieCommand request, CancellationToken cancellationToken)
        {           
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null) 
            {
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);
            }

            var movie = await _movieRepository.GetByTmdbIdAsync(request.TmdbId, cancellationToken);
            if(movie is null)
            {
                return Result.Fail(ApplicationErrors.Movie.NotFound);
            }
            var existingRating = movie.Ratings.FirstOrDefault(r => r.UserId == user.Id);

            var rateMovieResult = await _movieDomainService.RateMovieAsync(movie, user, request.Rating, cancellationToken);

            return rateMovieResult;
        }
    }
}
