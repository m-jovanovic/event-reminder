using System;
using System.Collections.Generic;
using EventReminder.Domain.Core.Events;

namespace EventReminder.Domain.Core.Primitives
{
    /// <summary>
    /// Represents the aggregate root.
    /// </summary>
    public abstract class AggregateRoot : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        /// <param name="id">The aggregate root identifier.</param>
        protected AggregateRoot(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        protected AggregateRoot()
        {
        }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        /// <summary>
        /// Gets the domain events. This collection is readonly.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Clears all the domain events from the <see cref="AggregateRoot"/>.
        /// </summary>
        public void ClearDomainEvents() => _domainEvents.Clear();
        
        /// <summary>
        /// Adds the specified <see cref="IDomainEvent"/> to the <see cref="AggregateRoot"/>.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
