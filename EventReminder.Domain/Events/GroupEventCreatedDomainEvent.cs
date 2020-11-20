using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when a group event is created.
    /// </summary>
    public sealed class GroupEventCreatedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventCreatedDomainEvent"/> class.
        /// </summary>
        /// <param name="groupEvent">The group event.</param>
        internal GroupEventCreatedDomainEvent(GroupEvent groupEvent) => GroupEvent = groupEvent;

        /// <summary>
        /// Gets the group event.
        /// </summary>
        public GroupEvent GroupEvent { get; }
    }
}
