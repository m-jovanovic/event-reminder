using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.FriendshipRequests;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.FriendshipRequests.GetSentFriendshipRequests
{
    /// <summary>
    /// Represents the query for getting the sent friendship requests for the user identifier.
    /// </summary>
    public sealed class GetSentFriendshipRequestsQuery : IQuery<Maybe<SentFriendshipRequestsListResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSentFriendshipRequestsQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier provider.</param>
        public GetSentFriendshipRequestsQuery(Guid userId) => UserId = userId;

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
