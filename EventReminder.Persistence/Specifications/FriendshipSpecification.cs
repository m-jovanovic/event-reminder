using System;
using System.Linq.Expressions;
using EventReminder.Domain.Friendships;
using EventReminder.Domain.Users;

namespace EventReminder.Persistence.Specifications
{
    /// <summary>
    /// Represents the specification for determining the friendship of two users.
    /// </summary>
    internal sealed class FriendshipSpecification : Specification<Friendship>
    {
        private readonly Guid _userId;
        private readonly Guid _friendId;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipSpecification"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        internal FriendshipSpecification(User user, User friend)
        {
            _userId = user.Id;
            _friendId = friend.Id;
        }

        /// <inheritdoc />
        internal override Expression<Func<Friendship, bool>> ToExpression() =>
            friendship => friendship.UserId == _userId && friendship.FriendId == _friendId;
    }
}
