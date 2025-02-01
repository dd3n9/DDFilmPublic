using DDFilm.Contracts.SessionMovies.Responses;
using DDFilm.Domain.Common.Errors;
using DDFilm.Domain.Repositories;
using DDFilm.Domain.Services;
using FluentResults;
using Mapster;
using MediatR;

namespace DDFilm.Application.SessionMovies.Commands.Choose
{
    public class ChooseSessionMovieCommandHandler : IRequestHandler<ChooseSessionMovieCommand, Result<ChooseSessionMovieResponse>>
    {
        private readonly SessionDomainService _sessionDomainService;
        private readonly IApplicationUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;

        public ChooseSessionMovieCommandHandler(SessionDomainService sessionDomainService,
            IApplicationUserRepository userRepository,
            ISessionRepository sessionRepository)
        {
            _sessionDomainService = sessionDomainService;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;

        }

        public async Task<Result<ChooseSessionMovieResponse>> Handle(ChooseSessionMovieCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if(user is null)
            {
                return Result.Fail(ApplicationErrors.ApplicationUser.NotFound);
            }

            var session = await _sessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
            if(session is null)
            {
                return Result.Fail(ApplicationErrors.Session.NotFound);
            }

            var result = await _sessionDomainService.ChooseRandomMovieAsync(session, user, cancellationToken);
            if (result.IsFailed)
                return Result.Fail<ChooseSessionMovieResponse>(result.Errors);

            var sessionResponse = result.Value.Adapt<ChooseSessionMovieResponse>();

            return sessionResponse;
        }
    }
}
