using System;
using System.Linq.Expressions;
using EventReminder.Domain.Entities;

namespace EventReminder.Persistence.Specifications
{
    /// <summary>
    /// Represents the specification for determining the unprocessed attendee.
    /// </summary>
    internal sealed class UnprocessedAttendeeSpecification : Specification<Attendee>
    {
        /// <inheritdoc />
        internal override Expression<Func<Attendee, bool>> ToExpression() => attendee => !attendee.Processed;
    }
}
