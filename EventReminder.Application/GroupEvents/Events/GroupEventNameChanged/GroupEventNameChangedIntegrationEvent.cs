using System;
using System.Text.Json.Serialization;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Events;

namespace EventReminder.Application.GroupEvents.Events.GroupEventNameChanged
{
    /// <summary>
    /// Represents the integration event that is raised when a group event name is changed.
    /// </summary>
    public sealed class GroupEventNameChangedIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventNameChangedIntegrationEvent"/> class.
        /// </summary>
        /// <param name="groupEventNameChangedDomainEvent">The group event name changed domain event.</param>
        internal GroupEventNameChangedIntegrationEvent(GroupEventNameChangedDomainEvent groupEventNameChangedDomainEvent)
        {
            GroupEventId = groupEventNameChangedDomainEvent.GroupEvent.Id;
            PreviousName = groupEventNameChangedDomainEvent.PreviousName;
        }

        [JsonConstructor]
        private GroupEventNameChangedIntegrationEvent(Guid groupEventId, string previousName)
        {
            GroupEventId = groupEventId;
            PreviousName = previousName;
        }

        /// <summary>
        /// Gets the group event identifier.
        /// </summary>
        public Guid GroupEventId { get; }

        /// <summary>
        /// Gets the previous group event name.
        /// </summary>
        public string PreviousName { get; }
    }
}
