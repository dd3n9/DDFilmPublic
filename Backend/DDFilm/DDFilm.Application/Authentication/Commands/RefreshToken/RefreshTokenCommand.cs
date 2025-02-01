using DDFilm.Application.DTO;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<AuthTokensDto>>;
}
