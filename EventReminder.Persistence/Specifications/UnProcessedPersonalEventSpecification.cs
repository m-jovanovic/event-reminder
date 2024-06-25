using System;
using System.Linq.Expressions;
using EventReminder.Domain.Events;

namespace EventReminder.Persistence.Specifications
{
    /// <summary>
    /// Represents the specification for determining the unprocessed personal event.
    /// </summary>
    internal sealed class UnProcessedPersonalEventSpecification : Specification<PersonalEvent>
    {
        /// <inheritdoc />
        internal override Expression<Func<PersonalEvent, bool>> ToExpression() => personalEvent => !personalEvent.Processed;
    }
}
