using System;

namespace EventReminder.Contracts.PersonalEvents
{
    /// <summary>
    /// Represents the create personal event request.
    /// </summary>
    public sealed class CreatePersonalEventRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the date and time.
        /// </summary>
        public DateTime DateTimeUtc { get; set; }
    }
}
