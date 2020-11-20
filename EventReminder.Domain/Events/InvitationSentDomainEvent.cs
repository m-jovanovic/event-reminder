using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when an invitation is sent.
    /// </summary>
    public sealed class InvitationSentDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvitationSentDomainEvent"/> class.
        /// </summary>
        /// <param name="invitation">The invitation.</param>
        internal InvitationSentDomainEvent(Invitation invitation) => Invitation = invitation;

        /// <summary>
        /// Gets the invitation.
        /// </summary>
        public Invitation Invitation { get; }
    }
}
