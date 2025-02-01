using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Commands.Delete
{
    public record DeleteSessionMovieCommand(Guid SessionId, 
        int TmdbId, 
        string UserId ) : IRequest<Result>;
}
