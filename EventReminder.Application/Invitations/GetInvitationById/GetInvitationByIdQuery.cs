using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.Invitations;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.Invitations.GetInvitationById
{
    /// <summary>
    /// Represents the query for getting the invitation by the identifier.
    /// </summary>
    public sealed class GetInvitationByIdQuery : IQuery<Maybe<InvitationResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetInvitationByIdQuery"/> class.
        /// </summary>
        /// <param name="invitationId">The invitation identifier.</param>
        public GetInvitationByIdQuery(Guid invitationId) => InvitationId = invitationId;

        /// <summary>
        /// Gets the invitation identifier.
        /// </summary>
        public Guid InvitationId { get; }
    }
}
