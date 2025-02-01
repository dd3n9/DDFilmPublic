using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.Services;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Logout
{
    public class LogoutSessionCommandHandler : IRequestHandler<LogoutSessionCommand, Result>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly SessionDomainService _sessionDomainService;

        public LogoutSessionCommandHandler(ISessionRepository sessionRepository,
            IApplicationUserRepository applicationUserRepository,
            SessionDomainService sessionDomainService
            )
        {
            _sessionRepository = sessionRepository;
            _applicationUserRepository = applicationUserRepository;
            _sessionDomainService = sessionDomainService;
        }

        public async Task<Result> Handle(LogoutSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if (session is null)
                return Result.Fail(ApplicationErrors.Session.NotFound);

            var user = await _applicationUserRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            var result = await _sessionDomainService.SessionLogoutAsync(session, user, cancellationToken);
            return result;
        }
    }
}
