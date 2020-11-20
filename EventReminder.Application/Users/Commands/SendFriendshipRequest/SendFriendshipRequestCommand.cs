using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.Users.Commands.SendFriendshipRequest
{
    /// <summary>
    /// Represents the send friendship request command.
    /// </summary>
    public sealed class SendFriendshipRequestCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendFriendshipRequestCommand"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        public SendFriendshipRequestCommand(Guid userId, Guid friendId)
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
