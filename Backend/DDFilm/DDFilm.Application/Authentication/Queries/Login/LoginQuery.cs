using DDFilm.Application.Authentication.Common;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email, 
        string Password
        ) : IRequest<Result<AuthenticationResult>>;
}
