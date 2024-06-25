using System;
using EventReminder.Domain.Core.Events;

namespace EventReminder.Domain.Events.DomainEvents
{
    /// <summary>
    /// Represents the event that is raised when the date and time of a group event is changed.
    /// </summary>
    public sealed class GroupEventDateAndTimeChangedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventDateAndTimeChangedDomainEvent"/> class.
        /// </summary>
        /// <param name="groupEvent">The group event.</param>
        /// <param name="previousDateAndTimeUtc">The previous date and time of the group event in UTC format.</param>
        internal GroupEventDateAndTimeChangedDomainEvent(GroupEvent groupEvent, DateTime previousDateAndTimeUtc)
        {
            GroupEvent = groupEvent;
            PreviousDateAndTimeUtc = previousDateAndTimeUtc;
        }

        /// <summary>
        /// Gets the group event.
        /// </summary>
        public GroupEvent GroupEvent { get; }

        /// <summary>
        /// Gets the previous date and time of the group event in UTC format.
        /// </summary>
        public DateTime PreviousDateAndTimeUtc { get; }
    }
}