using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Logout
{
    public record LogoutSessionCommand(Guid SessionId, string UserId) : IRequest<Result>;
}
