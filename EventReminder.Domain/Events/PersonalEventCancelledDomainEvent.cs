using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when a personal event is cancelled.
    /// </summary>
    public sealed class PersonalEventCancelledDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEventCancelledDomainEvent"/> class.
        /// </summary>
        /// <param name="personalEvent">The personal event.</param>
        internal PersonalEventCancelledDomainEvent(PersonalEvent personalEvent) => PersonalEvent = personalEvent;

        /// <summary>
        /// Gets the personal event.
        /// </summary>
        public PersonalEvent PersonalEvent { get; }
    }
}