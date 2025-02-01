using DDFilm.Contracts.Common;
using DDFilm.Contracts.SessionMovies.Responses;
using FluentResults;
using MediatR;


namespace DDFilm.Application.SessionMovies.Queries.GetWatchedSessionMovies
{
    public record GetWatchedSessionMoviesQuery(PaginationParams PaginationParams, 
        string UserId,
        Guid SessionId) : IRequest<Result<PaginatedList<SessionMovieResponse>>>;
}
