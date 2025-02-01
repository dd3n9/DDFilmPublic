using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Create
{
    public record CreateSessionCommand(
        string UserId,
        string SessionName,
        string Password
        ) : IRequest<Result>;
}
