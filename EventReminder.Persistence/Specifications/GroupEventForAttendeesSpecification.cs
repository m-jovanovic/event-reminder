using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EventReminder.Domain.Events;

namespace EventReminder.Persistence.Specifications
{
    /// <summary>
    /// Represents the specification for determining the group event the attendees will be attending.
    /// </summary>
    internal sealed class GroupEventForAttendeesSpecification : Specification<GroupEvent>
    {
        private readonly Guid[] _groupEventIds;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventForAttendeesSpecification"/> class.
        /// </summary>
        /// <param name="attendees">The attendees.</param>
        internal GroupEventForAttendeesSpecification(IReadOnlyCollection<Attendee> attendees) =>
            _groupEventIds = attendees.Select(attendee => attendee.EventId).Distinct().ToArray();

        /// <inheritdoc />
        internal override Expression<Func<GroupEvent, bool>> ToExpression() => groupEvent => _groupEventIds.Contains(groupEvent.Id);
    }
}
