using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<Result>;
}
