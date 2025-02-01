namespace DDFilm.Contracts.SessionParticipant.Responses
{
    public record SessionParticipantResponse(
        Guid SessionId, string UserName, string UserId, string Role );
}
