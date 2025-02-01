using DDFilm.Domain.ApplicationUserAggregate.ValueObjects;
using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;


namespace DDFilm.Domain.MovieAggregate.Events
{
    public record MovieRatingUpdated(SessionId? SessionId, ApplicationUserId ApplicationUserId, RatingValue RatingValue) : IDomainEvent;
}
