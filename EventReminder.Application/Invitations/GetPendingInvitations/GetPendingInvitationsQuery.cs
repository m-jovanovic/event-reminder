using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.Invitations;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.Invitations.GetPendingInvitations
{
    /// <summary>
    /// Represents the query for getting the pending invitations for the user identifier.
    /// </summary>
    public sealed class GetPendingInvitationsQuery : IQuery<Maybe<PendingInvitationsListResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPendingInvitationsQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier provider.</param>
        public GetPendingInvitationsQuery(Guid userId) => UserId = userId;

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
