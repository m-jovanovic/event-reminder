using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.Friendships;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.Friendships.Queries.GetFriendship
{
    /// <summary>
    /// Represents the query for getting the friendship for the user and friend identifiers.
    /// </summary>
    public sealed class GetFriendshipQuery : IQuery<Maybe<FriendshipResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFriendshipQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        public GetFriendshipQuery(Guid userId, Guid friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the friend identifier.
        /// </summary>
        public Guid FriendId { get; }
    }
}
