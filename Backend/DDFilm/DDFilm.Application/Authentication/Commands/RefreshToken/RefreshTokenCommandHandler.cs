using DDFilm.Application.Common.Interfaces.Authentication;
using DDFilm.Application.DTO;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthTokensDto>>
    {
        private readonly IAuthenticationService _authenticatinService;

        public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticatinService = authenticationService;
        }

        public async Task<Result<AuthTokensDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authenticatinService.RefreshTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);
        }
    }
}
