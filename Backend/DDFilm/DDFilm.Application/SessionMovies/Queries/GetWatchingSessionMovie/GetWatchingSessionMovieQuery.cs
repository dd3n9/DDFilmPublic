using DDFilm.Contracts.SessionMovies.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Queries.GetWatchingSessionMovie
{
    public record GetWatchingSessionMovieQuery(string UserId,
         Guid SessionId) : IRequest<Result<SessionMovieResponse>>;
}
