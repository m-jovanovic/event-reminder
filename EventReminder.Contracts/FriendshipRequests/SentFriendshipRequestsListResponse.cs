using System;
using System.Collections.Generic;

namespace EventReminder.Contracts.FriendshipRequests
{
    /// <summary>
    /// Represents the sent friendship requests list response.
    /// </summary>
    public sealed class SentFriendshipRequestsListResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SentFriendshipRequestsListResponse"/> class.
        /// </summary>
        /// <param name="friendshipRequests">The friendship requests.</param>
        public SentFriendshipRequestsListResponse(IReadOnlyCollection<SentFriendshipRequestModel> friendshipRequests)
            => FriendshipRequests = friendshipRequests;

        /// <summary>
        /// Gets the friendship requests.
        /// </summary>
        public IReadOnlyCollection<SentFriendshipRequestModel> FriendshipRequests { get; }

        /// <summary>
        /// Represents the friendship request model.
        /// </summary>
        public sealed class SentFriendshipRequestModel
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
