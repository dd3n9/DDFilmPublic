using DDFilm.Contracts.Sessions.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Queries.GetByName
{
    public record GetSessionByNameQuery(string SessionName) : IRequest<Result<SessionResponse>>;
}
