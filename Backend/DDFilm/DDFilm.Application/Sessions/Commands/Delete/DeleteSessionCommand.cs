using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Delete
{
    public record DeleteSessionCommand(string UserId, Guid SessionId) : IRequest<Result>;
}
