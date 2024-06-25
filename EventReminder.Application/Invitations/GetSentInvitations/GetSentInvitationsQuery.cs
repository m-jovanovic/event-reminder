using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.Invitations;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.Invitations.GetSentInvitations
{
    /// <summary>
    /// Represents the query for getting the sent invitations for the user identifier.
    /// </summary>
    public sealed class GetSentInvitationsQuery : IQuery<Maybe<SentInvitationsListResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSentInvitationsQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier provider.</param>
        public GetSentInvitationsQuery(Guid userId) => UserId = userId;

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
