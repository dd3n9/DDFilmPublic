using DDFilm.Application.Common.Interfaces.Authentication;
using DDFilm.Application.DTO;
using DDFilm.Contracts.Configurations;
using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.ApplicationUserAggregate.Entities;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DDFilm.Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IApplicationUserRepository applicationUserRepository, IOptions<JwtConfig> jwtConfig)
        {
            _applicationUserRepository = applicationUserRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _jwtConfig = jwtConfig.Value;

        }

        public async Task<Result<AuthTokensDto>> GenerateJwtTokenAsync(AuthenticationDto authenticationDto, CancellationToken cancellationToken)
        {
            var user = await _applicationUserRepository.GetByIdAsync(authenticationDto.UserId, cancellationToken);

            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            var authResult = await GenerateTokensForUserAsync(user);
            await _applicationUserRepository.UpdateAsync(user, cancellationToken);

            return Result.Ok(authResult);
        }

        public async Task<Result<AuthTokensDto>> RefreshTokenAsync(string accessToken, Token refreshToken, CancellationToken cancellationToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal is null)
                return Result.Fail(ApplicationErrors.Authentication.InvalidToken);

            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null || string.IsNullOrWhiteSpace(userIdClaim.Value))
                return Result.Fail(ApplicationErrors.Authentication.InvalidToken);

            var user = await _applicationUserRepository.GetByIdAsync(userIdClaim.Value, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            var storedRefreshToken = user.FindRefreshToken(refreshToken);
            if (storedRefreshToken is null || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
                return Result.Fail(ApplicationErrors.Authentication.RefreshTokenExpired);

            user.RemoveRefreshToken(storedRefreshToken);

            var newAuthResult = await GenerateTokensForUserAsync(user);
            await _applicationUserRepository.UpdateAsync(user, cancellationToken);

            return Result.Ok(newAuthResult);
        }

        private string ExtractJtiFromToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(Token token)
        {
            var secret = _jwtConfig.Secret;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience, 
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token.Value, tokenValidationParameters, out _); 
        }
        
        private async Task<AuthTokensDto> GenerateTokensForUserAsync(ApplicationUser user)
        {
            var authenticationDto = new AuthenticationDto(user.Id, user.FirstName, user.LastName, user.UserName);
            var jwtTokenValue = await _jwtTokenGenerator.GenerateToken(authenticationDto);
            var refreshToken = RefreshToken.Create(ExtractJtiFromToken(jwtTokenValue),
                DateTime.UtcNow,
                DateTime.UtcNow.AddMonths(_jwtConfig.RefreshTokenExpiryMonths));

            user.AddRefreshToken(refreshToken);

            return new AuthTokensDto(jwtTokenValue, refreshToken.Token);
        }

        public async Task<Result> RevokeAllRefreshTokensAsync(ApplicationUserId applicationUserId, CancellationToken cancellationToken)
        {
            var user = await _applicationUserRepository.GetByIdAsync(applicationUserId, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            user.RevokeAllRefreshTokens();

            await _applicationUserRepository.UpdateAsync(user, cancellationToken);

            return Result.Ok();
        }
    }
}
