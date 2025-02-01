﻿using DDFilm.Domain.Common.Exceptions;

namespace DDFilm.Domain.Common.Models
{
    public abstract record AggregateRootId<TId>
    {
        public TId Value { get; protected set; }

        protected AggregateRootId(TId value)
        {
            if (value is null)
            {
                throw new EmptyAggregateRootIdException();
            }
            Value = value;
        }
    }
}
