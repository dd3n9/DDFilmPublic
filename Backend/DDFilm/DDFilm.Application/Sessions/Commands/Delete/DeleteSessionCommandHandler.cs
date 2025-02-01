using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.Services;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Delete
{
    public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, Result>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly SessionDomainService _sessionDomainService;

        public DeleteSessionCommandHandler(ISessionRepository sessionRepository, 
            IApplicationUserRepository userRepository, 
            SessionDomainService sessionDomainService)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _sessionDomainService = sessionDomainService;
        }

        public async Task<Result> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            var (userId, sessionId) = request;

            var session = await _sessionRepository.GetByIdAsync(sessionId, cancellationToken);
            if(session is null)
            {
                return Result.Fail(ApplicationErrors.Session.NotFound);
            }

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if(user is null)
            {
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);
            }

            var result = await _sessionDomainService.DeleteSessionAsync(session, user, cancellationToken);
            return result;
        }
    }
}
