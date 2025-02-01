using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.Services;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Login
{
    public class LoginSessionCommandHandler : IRequestHandler<LoginSessionCommand, Result>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly SessionDomainService _sessionDomainService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginSessionCommandHandler(ISessionRepository sessionRepository,
            IApplicationUserRepository applicationUserRepository,
            SessionDomainService sessionDomainService,
            IPasswordHasher passwordHasher
            )
        {
            _sessionRepository = sessionRepository;
            _applicationUserRepository = applicationUserRepository;
            _sessionDomainService = sessionDomainService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(LoginSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _sessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if (session is null)
                return Result.Fail(ApplicationErrors.Session.NotFound);

            var user = await _applicationUserRepository.GetByIdAsync(request.UserId, cancellationToken);    
            if(user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            if (!_passwordHasher.Verify(session.Password, request.Password))
                return Result.Fail(ApplicationErrors.Session.IncorrectPassword);

            var result = await _sessionDomainService.SessionLoginAsync(session, user, cancellationToken);
            return result;
        }
    }
}
