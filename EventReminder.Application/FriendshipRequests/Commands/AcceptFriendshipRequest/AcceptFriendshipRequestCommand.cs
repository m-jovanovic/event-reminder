using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.FriendshipRequests.Commands.AcceptFriendshipRequest
{
    /// <summary>
    /// Represents the accept friendship request command.
    /// </summary>
    public sealed class AcceptFriendshipRequestCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptFriendshipRequestCommand"/> class.
        /// </summary>
        /// <param name="friendshipRequestId">The friendship request identifier.</param>
        public AcceptFriendshipRequestCommand(Guid friendshipRequestId) => FriendshipRequestId = friendshipRequestId;

        /// <summary>
        /// Gets the friendship request identifier.
        /// </summary>
        public Guid FriendshipRequestId { get; }
    }
}
