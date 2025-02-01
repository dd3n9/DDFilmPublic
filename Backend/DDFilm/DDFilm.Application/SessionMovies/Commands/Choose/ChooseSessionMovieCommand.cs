using DDFilm.Contracts.SessionMovies.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionMovies.Commands.Choose
{
    public record ChooseSessionMovieCommand(Guid SessionId, 
        string UserId
        ) : IRequest<Result<ChooseSessionMovieResponse>>;
}
