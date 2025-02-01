using FluentResults;
using MediatR;

namespace DDFilm.Application.Movies.Commands.RateMovie
{
    public record RateMovieCommand(
        int TmdbId, 
        string UserId,
        int Rating
        ) : IRequest<Result>;
}
