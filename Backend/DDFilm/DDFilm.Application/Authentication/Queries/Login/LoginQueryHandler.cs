using DDFilm.Application.Authentication.Common;
using DDFilm.Application.Common.Interfaces.Authentication;
using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Application.DTO;
using DDFilm.Domain.Common.Errors;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationUserReadService _userReadService;
        private readonly IPasswordHasher _passwordHasher;
      

        public LoginQueryHandler(IAuthenticationService authenticationService,
            IApplicationUserReadService userReadService,
            IPasswordHasher passwordHasher
            )
        {
            _authenticationService = authenticationService;
            _userReadService = userReadService;
            _passwordHasher = passwordHasher;
           
        }

        public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userReadService.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            if (!_passwordHasher.Verify(user.Password, request.Password))
                return Result.Fail(ApplicationErrors.Authentication.IncorrectPassword);

            var authDto = new AuthenticationDto(user.Id, user.FirstName, user.LastName, user.UserName);

            var token = await _authenticationService.GenerateJwtTokenAsync(authDto, cancellationToken);
            if (token.IsFailed)
                return Result.Fail(token.Errors);

            var result = new AuthenticationResult(authDto, token.Value.Token, token.Value.RefreshToken);

            return Result.Ok(result);
        }
    }
}
