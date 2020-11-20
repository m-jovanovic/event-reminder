using System;

namespace EventReminder.Contracts.Users
{
    /// <summary>
    /// Represents the send friendship request request.
    /// </summary>
    public sealed class SendFriendshipRequestRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        public Guid FriendId { get; set; }
    }
}
