using System;
using EventReminder.Application.Abstractions.Messaging;

namespace EventReminder.Application.Attendees.AttendeeCreated
{
    /// <summary>
    /// Represents the event that is published when an attendee is created.
    /// </summary>
    public sealed class AttendeeCreatedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCreatedEvent"/> class.
        /// </summary>
        /// <param name="attendeeId">The attendee identifier.</param>
        public AttendeeCreatedEvent(Guid attendeeId) => AttendeeId = attendeeId;

        /// <summary>
        /// Gets the attendee identifier.
        /// </summary>
        public Guid AttendeeId { get; }
    }
}
