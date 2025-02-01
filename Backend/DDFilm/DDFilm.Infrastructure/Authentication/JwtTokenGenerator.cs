using DDFilm.Application.Common.Interfaces.Authentication;
using DDFilm.Application.DTO;
using DDFilm.Contracts.Configurations;
using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DDFilm.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IUserRoleService _userRoleService;
        private readonly JwtConfig _jwtConfig;

        public JwtTokenGenerator(IUserRoleService userRoleService, 
            IOptions<JwtConfig> jwtConfig)
        {
            _userRoleService = userRoleService;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<string> GenerateToken(AuthenticationDto authenticationDto)
        {
            var userRoles = await _userRoleService.GetUserRolesAsync(new ApplicationUserId(authenticationDto.UserId));

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, authenticationDto.UserId),
                new Claim(JwtRegisteredClaimNames.GivenName, authenticationDto.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, authenticationDto.LastName),
                new Claim(JwtRegisteredClaimNames.UniqueName, authenticationDto.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var signingCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpiryMinutes),
                notBefore: DateTime.UtcNow,
                claims: authClaims,
                signingCredentials: signingCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }
    }
}
