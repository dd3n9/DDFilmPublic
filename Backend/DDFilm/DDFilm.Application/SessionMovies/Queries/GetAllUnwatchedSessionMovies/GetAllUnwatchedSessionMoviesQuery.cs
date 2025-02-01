using DDFilm.Contracts.SessionMovies.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Queries.GetAllUnwatchedSessionMovies
{
    public record GetAllUnwatchedSessionMoviesQuery(string UserId,
        Guid SessionId) : IRequest<Result<IEnumerable<SessionMovieResponse>>>;
}
