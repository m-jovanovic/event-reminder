using System;

namespace EventReminder.Contracts.GroupEvents
{
    /// <summary>
    /// Represents the invite friend to group event request.
    /// </summary>
    public sealed class InviteFriendToGroupEventRequest
    {
        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        public Guid GroupEventId { get; set; }

        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        public Guid FriendId { get; set; }
    }
}
