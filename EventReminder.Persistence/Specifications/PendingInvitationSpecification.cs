using System;
using System.Linq.Expressions;
using EventReminder.Domain.Entities;

namespace EventReminder.Persistence.Specifications
{
    /// <summary>
    /// Represents the specification for determining the pending invitation.
    /// </summary>
    internal sealed class PendingInvitationSpecification : Specification<Invitation>
    {
        private readonly Guid _groupEventId;
        private readonly Guid _userId;

        internal PendingInvitationSpecification(GroupEvent groupEvent, User user)
        {
            _groupEventId = groupEvent.Id;
            _userId = user.Id;
        }

        /// <inheritdoc />
        internal override Expression<Func<Invitation, bool>> ToExpression() =>
            invitation => invitation.CompletedOnUtc == null &&
                          invitation.EventId == _groupEventId &&
                          invitation.UserId == _userId;
    }
}
