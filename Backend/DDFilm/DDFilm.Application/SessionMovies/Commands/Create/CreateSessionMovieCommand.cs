using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Commands.Create
{
    public record CreateSessionMovieCommand(
        string UserId, 
        Guid SessionId, 
        int TmdbId,
        string MovieTitle
        ) : IRequest<Result>;
}
