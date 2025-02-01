﻿
namespace DDFilm.Domain.Common.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }

        protected AggregateRoot() { }

        protected void AddEvent(IDomainEvent domainEvent)
        {
            AddDomainEvents(domainEvent); 
        }
    }
}
