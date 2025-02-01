using DDFilm.Application.DTO;

namespace DDFilm.Application.Authentication.Common
{
    public record AuthenticationResult(AuthenticationDto AuthenticationDto, string Token, string RefreshToken);
}
