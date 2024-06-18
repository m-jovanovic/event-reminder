using System;
using System.Text.Json.Serialization;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Events;

namespace EventReminder.Application.GroupEvents.Events.GroupEventDateAndTimeChanged
{
    /// <summary>
    /// Represents the event that is raised when a group event date and time is changed.
    /// </summary>
    public sealed class GroupEventDateAndTimeChangedIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventDateAndTimeChangedIntegrationEvent"/> class.
        /// </summary>
        /// <param name="groupEventDateAndTimeChangedDomainEvent">The group event date and time changed domain event.</param>
        internal GroupEventDateAndTimeChangedIntegrationEvent(
            GroupEventDateAndTimeChangedDomainEvent groupEventDateAndTimeChangedDomainEvent)
        {
            GroupEventId = groupEventDateAndTimeChangedDomainEvent.GroupEvent.Id;
            PreviousDateAndTimeUtc = groupEventDateAndTimeChangedDomainEvent.PreviousDateAndTimeUtc;
        }

        [JsonConstructor]
        private GroupEventDateAndTimeChangedIntegrationEvent(Guid groupEventId, DateTime previousDateAndTimeUtc)
        {
            GroupEventId = groupEventId;
            PreviousDateAndTimeUtc = previousDateAndTimeUtc;
        }

        /// <summary>
        /// Gets the group event identifier.
        /// </summary>
        public Guid GroupEventId { get; }

        /// <summary>
        /// Gets the previous group event date and time in UTC format.
        /// </summary>
        public DateTime PreviousDateAndTimeUtc { get; }
    }
}
