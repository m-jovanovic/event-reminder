using System;
using EventReminder.Application.Core.Abstractions.Messaging;

namespace EventReminder.Application.Attendees.Events.AttendeeCreated
{
    /// <summary>
    /// Represents the event that is raised when an attendee is created.
    /// </summary>
    public sealed class AttendeeCreatedIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCreatedIntegrationEvent"/> class.
        /// </summary>
        /// <param name="attendeeCreatedEvent">The attendee created event.</param>
        internal AttendeeCreatedIntegrationEvent(AttendeeCreatedEvent attendeeCreatedEvent) =>
            AttendeeId = attendeeCreatedEvent.AttendeeId;

        /// <summary>
        /// Gets the attendee identifier.
        /// </summary>
        public Guid AttendeeId { get; }
    }
}
