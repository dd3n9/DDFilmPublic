using DDFilm.Contracts.SessionMovies.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Queries.GetSessionMovies
{
    public record GetSessionMoviesQuery(string UserId, Guid SessionId) : IRequest<Result<IEnumerable<SessionMovieResponse>>>;
}
