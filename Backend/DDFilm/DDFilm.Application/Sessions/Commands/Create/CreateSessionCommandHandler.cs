using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Factories.Sessions;
using DDFilm.Domain.Repositories;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Sessions.Commands.Create
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Result>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionReadService _sessionReadService;
        private readonly ISessionFactory _factory;
        private readonly IApplicationUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;


        public CreateSessionCommandHandler(ISessionRepository sessionRepository,
            ISessionReadService sessionReadService,
            ISessionFactory factory,
            IApplicationUserRepository userRepository,
            IPasswordHasher passwordHasher
           )
        {
            _sessionRepository = sessionRepository;
            _sessionReadService = sessionReadService;
            _factory = factory;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var (userId, sessionName, password) = request;
            if (await _sessionReadService.ExistsByNameAsync(sessionName, cancellationToken))
            {
                return Result.Fail(ApplicationErrors.Session.AlreadyExists);
            }

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user is null)
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);

            var hashedPassword = _passwordHasher.Hash(password);

            var session = await _factory.Create(user, sessionName, hashedPassword);
            await _sessionRepository.AddAsync(session, cancellationToken);

            return Result.Ok();
        }
    }
}
