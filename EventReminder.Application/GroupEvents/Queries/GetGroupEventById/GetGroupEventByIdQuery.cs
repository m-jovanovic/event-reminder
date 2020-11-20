using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.GroupEvents;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.GroupEvents.Queries.GetGroupEventById
{
    /// <summary>
    /// Represents the query for getting the group event by identifier.
    /// </summary>
    public sealed class GetGroupEventByIdQuery : IQuery<Maybe<DetailedGroupEventResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGroupEventByIdQuery"/> class.
        /// </summary>
        /// <param name="groupEventId">The group event identifier.</param>
        public GetGroupEventByIdQuery(Guid groupEventId) => GroupEventId = groupEventId;

        /// <summary>
        /// Gets the group event identifier.
        /// </summary>
        public Guid GroupEventId { get; }
    }
}
