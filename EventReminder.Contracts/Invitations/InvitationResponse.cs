using System;

namespace EventReminder.Contracts.Invitations
{
    /// <summary>
    /// Represents the invitation response.
    /// </summary>
    public sealed class InvitationResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the friend name.
        /// </summary>
        public string FriendName { get; set; }

        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; }
        
        /// <summary>
        /// Gets or sets the event date and time in UTC format.
        /// </summary>
        public DateTime EventDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the created on date and time in UTC format.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
