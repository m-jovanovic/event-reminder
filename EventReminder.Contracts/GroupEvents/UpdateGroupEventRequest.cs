using System;

namespace EventReminder.Contracts.GroupEvents
{
    /// <summary>
    /// Represents the update group event request.
    /// </summary>
    public sealed class UpdateGroupEventRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid GroupEventId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date and time.
        /// </summary>
        public DateTime DateTimeUtc { get; set; }
    }
}