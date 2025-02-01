using DDFilm.Contracts.Common;
using DDFilm.Contracts.Sessions.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Queries.GetMySessions
{
    public record GetMySessionsQuery(PaginationParams PaginationParams, string UserId) : IRequest<Result<PaginatedList<SessionResponse>>>;
}
