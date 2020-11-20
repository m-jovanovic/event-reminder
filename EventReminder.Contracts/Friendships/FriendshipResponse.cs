using System;

namespace EventReminder.Contracts.Friendships
{
    /// <summary>
    /// Represents the friendship response.
    /// </summary>
    public sealed class FriendshipResponse
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        public Guid FriendId { get; set; }

        /// <summary>
        /// Gets or sets the friend email.
        /// </summary>
        public string FriendEmail { get; set; }

        /// <summary>
        /// Gets or sets the friend name.
        /// </summary>
        public string FriendName { get; set; }

        /// <summary>
        /// Gets or sets the created on date and time in UTC format.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
