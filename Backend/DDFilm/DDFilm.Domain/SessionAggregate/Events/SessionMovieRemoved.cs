using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.ValueObjects;

namespace DDFilm.Domain.SessionAggregate.Events
{
    public record SessionMovieRemoved(Session Session, SessionMovieId SessionMovieId) : IDomainEvent;
}
