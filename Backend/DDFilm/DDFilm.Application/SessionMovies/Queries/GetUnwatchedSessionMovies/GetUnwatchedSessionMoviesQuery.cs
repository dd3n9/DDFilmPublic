using DDFilm.Contracts.Common;
using DDFilm.Contracts.SessionMovies.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Queries.GetUnwatchedSessionMovies
{
    public record GetUnwatchedSessionMoviesQuery(PaginationParams PaginationParams, 
        string UserId,
        Guid SessionId) : IRequest<Result<PaginatedList<SessionMovieResponse>>>;
}
