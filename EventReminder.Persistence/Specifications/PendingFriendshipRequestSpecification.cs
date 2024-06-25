using System;
using System.Linq.Expressions;
using EventReminder.Domain.Friendships;
using EventReminder.Domain.Users;

namespace EventReminder.Persistence.Specifications
{
    /// <summary>
    /// Represents the specification for determining the pending friendship request.
    /// </summary>
    internal sealed class PendingFriendshipRequestSpecification : Specification<FriendshipRequest>
    {
        private readonly Guid _userId;
        private readonly Guid _friendId;

        /// <summary>
        /// Initializes a new instance of the <see cref="PendingFriendshipRequestSpecification"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        internal PendingFriendshipRequestSpecification(User user, User friend)
        {
            _userId = user.Id;
            _friendId = friend.Id;
        }

        /// <inheritdoc />
        internal override Expression<Func<FriendshipRequest, bool>> ToExpression() =>
            friendshipRequest => (friendshipRequest.UserId == _userId || friendshipRequest.UserId == _friendId) &&
                                 (friendshipRequest.FriendId == _userId || friendshipRequest.FriendId == _friendId) &&
                                 friendshipRequest.CompletedOnUtc == null;
    }
}
