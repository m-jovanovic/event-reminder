using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.Attendees;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.Attendees.Queries.GetAttendeesForEventId
{
    /// <summary>
    /// Represents the query for getting group event attendees.
    /// </summary>
    public sealed class GetAttendeesForGroupEventIdQuery : IQuery<Maybe<AttendeeListResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttendeesForGroupEventIdQuery"/> class.
        /// </summary>
        /// <param name="groupEventId">The group event identifier.</param>
        public GetAttendeesForGroupEventIdQuery(Guid groupEventId) => GroupEventId = groupEventId;
        
        /// <summary>
        /// Gets the group event identifier.
        /// </summary>
        public Guid GroupEventId { get; }
    }
}
