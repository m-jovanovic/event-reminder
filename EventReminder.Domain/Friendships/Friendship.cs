using System;
using EventReminder.Domain.Core.Abstractions;
using EventReminder.Domain.Core.Guards;
using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Users;

namespace EventReminder.Domain.Friendships
{
    /// <summary>
    /// Represents the friendship.
    /// </summary>
    public sealed class Friendship : Entity, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Friendship"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        public Friendship(User user, User friend)
        {
            Ensure.NotNull(user, "The user is required.", nameof(user));
            Ensure.NotEmpty(user.Id, "The user identifier is required.", $"{nameof(user)}{nameof(user.Id)}");
            Ensure.NotNull(friend, "The friend is required.", nameof(friend));
            Ensure.NotEmpty(friend.Id, "The friend identifier is required.", $"{nameof(friend)}{nameof(friend.Id)}");

            UserId = user.Id;
            FriendId = friend.Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Friendship"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Friendship()
        {
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the friend identifier.
        /// </summary>
        public Guid FriendId { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }
    }
}
