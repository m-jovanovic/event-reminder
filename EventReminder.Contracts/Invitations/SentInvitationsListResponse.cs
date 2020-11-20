using System;
using System.Collections.Generic;

namespace EventReminder.Contracts.Invitations
{
    /// <summary>
    /// Represents the sent invitations list response.
    /// </summary>
    public sealed class SentInvitationsListResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SentInvitationsListResponse"/> class.
        /// </summary>
        /// <param name="invitations">The friendship requests.</param>
        public SentInvitationsListResponse(IReadOnlyCollection<SentInvitationModel> invitations) => Invitations = invitations;

        /// <summary>
        /// Gets the invitations.
        /// </summary>
        public IReadOnlyCollection<SentInvitationModel> Invitations { get; }

        /// <summary>
        /// Represents the invitation model.
        /// </summary>
        public sealed class SentInvitationModel
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            public Guid Id { get; set; }
            
            /// <summary>
            /// Gets or sets the friend identifier.
            /// </summary>
            public Guid FriendId { get; set; }

            /// <summary>
            /// Gets or sets the friend name.
            /// </summary>
            public string FriendName { get; set; }

            /// <summary>
            /// Gets or sets the event name.
            /// </summary>
            public string EventName { get; set; }

            /// <summary>
            /// Gets or sets the event date and time in UTC format.
            /// </summary>
            public DateTime EventDateTimeUtc { get; set; }

            /// <summary>
            /// Gets or sets the created on date and time in UTC format.
            /// </summary>
            public DateTime CreatedOnUtc { get; set; }
        }
    }
}
