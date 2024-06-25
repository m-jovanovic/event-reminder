using EventReminder.Domain.Core.Events;

namespace EventReminder.Domain.Events.DomainEvents
{
    /// <summary>
    /// Represents the event that is raised when the date and time of a personal event is changed.
    /// </summary>
    public sealed class PersonalEventDateAndTimeChangedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEventDateAndTimeChangedDomainEvent"/> class.
        /// </summary>
        /// <param name="personalEvent">The personal event.</param>
        internal PersonalEventDateAndTimeChangedDomainEvent(PersonalEvent personalEvent) => PersonalEvent = personalEvent;

        /// <summary>
        /// Gets the personal event.
        /// </summary>
        public PersonalEvent PersonalEvent { get; }
    }
}
