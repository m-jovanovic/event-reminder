using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.PersonalEvents;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.PersonalEvents.Queries.GetPersonalEventById
{
    /// <summary>
    /// Represents the query for getting the personal event by identifier.
    /// </summary>
    public sealed class GetPersonalEventByIdQuery : IQuery<Maybe<DetailedPersonalEventResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonalEventByIdQuery"/> class.
        /// </summary>
        /// <param name="personalEventId">The personal event identifier.</param>
        public GetPersonalEventByIdQuery(Guid personalEventId) => PersonalEventId = personalEventId;

        /// <summary>
        /// Gets the personal event identifier.
        /// </summary>
        public Guid PersonalEventId { get; }
    }
}
