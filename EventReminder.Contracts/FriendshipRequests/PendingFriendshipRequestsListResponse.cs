using System;
using System.Collections.Generic;

namespace EventReminder.Contracts.FriendshipRequests
{
    /// <summary>
    /// Represents the pending friendship requests list response.
    /// </summary>
    public sealed class PendingFriendshipRequestsListResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PendingFriendshipRequestsListResponse"/> class.
        /// </summary>
        /// <param name="friendshipRequests">The friendship requests.</param>
        public PendingFriendshipRequestsListResponse(IReadOnlyCollection<PendingFriendshipRequestModel> friendshipRequests)
            => FriendshipRequests = friendshipRequests;

        /// <summary>
        /// Gets the friendship requests.
        /// </summary>
        public IReadOnlyCollection<PendingFriendshipRequestModel> FriendshipRequests { get; }

        /// <summary>
        /// Represents the friendship request model.
        /// </summary>
        public sealed class PendingFriendshipRequestModel
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
            /// Gets or sets the created on date and time in UTC format.
            /// </summary>
            public DateTime CreatedOnUtc { get; set; }
        }
    }
}
