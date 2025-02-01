using DDFilm.Domain.Common.Models;
using DDFilm.Domain.SessionAggregate.Entities;

namespace DDFilm.Domain.SessionAggregate.Events
{
    public record SessionMovieAdded(Session session, SessionMovie sessionMovie) : IDomainEvent;
}
