using DDFilm.Api.Extensions;
using DDFilm.Application.Authentication.Commands.RefreshToken;
using DDFilm.Application.Authentication.Commands.Register;
using DDFilm.Application.Authentication.Commands.RevokeAllRefreshTokens;
using DDFilm.Application.Authentication.Commands.SeedRoles;
using DDFilm.Application.Authentication.Queries.Login;
using DDFilm.Contracts.Authentication;
using DDFilm.Contracts.Configurations;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace DDFilm.Api.Controllers
{
    [Route("api/account")]
    public class AuthenticationController : BaseController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly CookiesConfig _cookiesConfig;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationController(
            ISender mediator,
            IMapper mapper,
            IOptions<CookiesConfig> cookiesConfig,
            IOptions<JwtConfig> jwtConfig
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _cookiesConfig = cookiesConfig.Value;
            _jwtConfig = jwtConfig.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var result = await _mediator.Send( new SeedRolesCommand());

            return OkOrNotFound(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var registeResult = await _mediator.Send(command);

            return OkOrNotFound(registeResult);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginQuery>(request);
            var authResult = await _mediator.Send(command);

            var test = Request.Cookies[_cookiesConfig.CookiesKey];

            if (authResult.IsSuccess)
            {
                HttpContext.Response.Cookies.Append(_cookiesConfig.CookiesKey, authResult.Value.RefreshToken, 
                    new CookieOptions 
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMonths(_jwtConfig.RefreshTokenExpiryMonths)
                    });


                var response = new
                {
                    authResult.Value.AuthenticationDto,
                    authResult.Value.Token
                };
                
                return OkOrNotFound(Result.Ok(response));
            }

            return OkOrNotFound(authResult);
        }

        [AllowAnonymous]
        [Route("refreshToken")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();

            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Access token is missing or invalid.");
            }

            var command = new RefreshTokenCommand(accessToken, Request.Cookies[_cookiesConfig.CookiesKey]);

            var authResult = await _mediator.Send(command, cancellationToken);

            if(authResult.IsSuccess)
            {
                HttpContext.Response.Cookies.Append(_cookiesConfig.CookiesKey, authResult.Value.RefreshToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddMonths(_jwtConfig.RefreshTokenExpiryMonths)
                    });
                return OkOrNotFound(Result.Ok(authResult.Value.Token));
            }

            return OkOrNotFound(authResult);
        }

        [Route("revoke")]
        [HttpDelete]
        public async Task<IActionResult> RevokeAllRefreshTokens(CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserIdClaimValue();

            var command = new RevokeAllRefreshTokensCommand(userId);

            var result = await _mediator.Send(command, cancellationToken);

            return OkOrNotFound(result);
        }
    }
}
