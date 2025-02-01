using DDFilm.Contracts.Common;
using DDFilm.Contracts.Sessions.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Queries.Sessions
{
    public record GetSessionsQuery(PaginationParams PaginationParams) : IRequest<Result<PaginatedList<SessionResponse>>>;
}
