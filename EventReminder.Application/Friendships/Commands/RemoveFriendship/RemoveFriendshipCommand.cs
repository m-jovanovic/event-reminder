using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.Friendships.Commands.RemoveFriendship
{
    /// <summary>
    /// Represents the remove friendship command.
    /// </summary>
    public sealed class RemoveFriendshipCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveFriendshipCommand"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        public RemoveFriendshipCommand(Guid userId, Guid friendId)
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
