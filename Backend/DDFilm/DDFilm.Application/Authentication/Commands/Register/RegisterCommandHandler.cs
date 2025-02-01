using DDFilm.Application.Common.Interfaces.Authentication;
using DDFilm.Application.Common.Interfaces.Services;
using DDFilm.Application.DTO;
using DDFilm.Domain.ApplicationUserAggregate;
using DDFilm.Domain.Common.Constants;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using FluentResults;
using MediatR;

namespace DDFilm.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IApplicationUserReadService _userReadService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRoleService _userRoleService;

        public RegisterCommandHandler(IApplicationUserRepository userRepository,
            IApplicationUserReadService userReadService, 
            IPasswordHasher passwordHasher,
            IUserRoleService userRoleService)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
            _passwordHasher = passwordHasher;
            _userRoleService = userRoleService;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var (userName, firstName, lastName, email, password) = request;
            if (await _userReadService.ExistsByEmailAsync(request.Email, cancellationToken))
                return Result.Fail(ApplicationErrors.ApplicationUser.AlreadyExistsByEmail);

            var hashedPassword = _passwordHasher.Hash(password);

            var user = ApplicationUser.Create(userName, firstName, lastName, email, hashedPassword);

            var createUserResult = await _userRepository.AddAsync(user, cancellationToken);
            if(createUserResult.IsFailed)
            {
                return createUserResult;
            }

            await _userRoleService.AssignRoleAsync(user, StaticApplicationUserRoles.USER);

            return Result.Ok();
        }
    }
}
