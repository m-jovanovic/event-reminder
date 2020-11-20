using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when an invitation is accepted.
    /// </summary>
    public sealed class InvitationAcceptedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvitationAcceptedDomainEvent"/> class.
        /// </summary>
        /// <param name="invitation">The invitation.</param>
        internal InvitationAcceptedDomainEvent(Invitation invitation) => Invitation = invitation;

        /// <summary>
        /// Gets the invitation.
        /// </summary>
        public Invitation Invitation { get; }
    }
}
