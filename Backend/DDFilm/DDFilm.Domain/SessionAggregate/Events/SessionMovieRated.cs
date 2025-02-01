using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.SessionAggregate.Events
{
    public record SessionMovieRated(SessionId SessionId, SessionMovieId MovieId, string UserId, RatingValue Rating) : IDomainEvent;
}
