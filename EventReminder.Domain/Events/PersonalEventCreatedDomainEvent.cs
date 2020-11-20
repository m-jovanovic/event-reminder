using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event that is a raised when a new personal event is created.
    /// </summary>
    public sealed class PersonalEventCreatedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEventCreatedDomainEvent"/> class.
        /// </summary>
        /// <param name="personalEvent">The personal event.</param>
        internal PersonalEventCreatedDomainEvent(PersonalEvent personalEvent) => PersonalEvent = personalEvent;

        /// <summary>
        /// Gets the personal event.
        /// </summary>
        public PersonalEvent PersonalEvent { get; }

    }
}
