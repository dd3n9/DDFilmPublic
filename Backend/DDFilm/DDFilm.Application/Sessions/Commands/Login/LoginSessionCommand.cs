using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Login
{
    public record LoginSessionCommand(Guid SessionId, string UserId, string Password) : IRequest<Result>;
}
