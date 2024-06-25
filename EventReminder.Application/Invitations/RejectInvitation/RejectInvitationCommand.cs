using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.Invitations.RejectInvitation
{
    /// <summary>
    /// Represents the reject invitation command.
    /// </summary>
    public sealed class RejectInvitationCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RejectInvitationCommand"/> class.
        /// </summary>
        /// <param name="invitationId">The invitation identifier.</param>
        public RejectInvitationCommand(Guid invitationId) => InvitationId = invitationId;

        /// <summary>
        /// Gets the invitation identifier.
        /// </summary>
        public Guid InvitationId { get; }
    }
}
