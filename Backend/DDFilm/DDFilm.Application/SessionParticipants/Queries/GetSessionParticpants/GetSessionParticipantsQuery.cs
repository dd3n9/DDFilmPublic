using DDFilm.Contracts.SessionParticipant.Responses;
using FluentResults;
using MediatR;

namespace DDFilm.Application.SessionParticipants.Queries.GetSessionParticpants
{
    public record GetSessionParticipantsQuery(Guid SessionId,
        string UserId ) : IRequest<Result<IEnumerable<SessionParticipantResponse>>>;
}
