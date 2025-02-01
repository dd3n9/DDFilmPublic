using DDFilm.Application.Common.Interfaces.Authentication;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Commands.RevokeAllRefreshTokens
{
    public class RevokeAllRefreshTokensCommandHandler : IRequestHandler<RevokeAllRefreshTokensCommand, Result>
    {
        private readonly IAuthenticationService _authenticationService;

        public RevokeAllRefreshTokensCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(RevokeAllRefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RevokeAllRefreshTokensAsync(request.UserId, cancellationToken);

            return result;
        }
    }
}
