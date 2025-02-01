using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Commands.RevokeAllRefreshTokens
{
    public record RevokeAllRefreshTokensCommand(string UserId) : IRequest<Result>;
}
