using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.Invitations.Commands.AcceptInvitation
{
    /// <summary>
    /// Represents the accept invitation command.
    /// </summary>
    public sealed class AcceptInvitationCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptInvitationCommand"/> class.
        /// </summary>
        /// <param name="invitationId">The invitation identifier.</param>
        public AcceptInvitationCommand(Guid invitationId) => InvitationId = invitationId;

        /// <summary>
        /// Gets the invitation identifier.
        /// </summary>
        public Guid InvitationId { get; }
    }
}
