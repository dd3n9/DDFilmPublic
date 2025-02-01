using DDFilm.Application.DTO;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using FluentResults;

namespace DDFilm.Application.Common.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<Result<AuthTokensDto>> GenerateJwtTokenAsync(AuthenticationDto authenticationDto, CancellationToken cancellationToken);
        Task<Result<AuthTokensDto>> RefreshTokenAsync(string accessToken, Token refreshToken, CancellationToken cancellationToken);
        Task<Result> RevokeAllRefreshTokensAsync(ApplicationUserId applicationUserId, CancellationToken cancellationToken);
    }
}
