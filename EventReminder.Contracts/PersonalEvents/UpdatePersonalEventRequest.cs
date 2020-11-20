using System;

namespace EventReminder.Contracts.PersonalEvents
{
    /// <summary>
    /// Represents the update personal event request.
    /// </summary>
    public sealed class UpdatePersonalEventRequest
    {
        /// <summary>
        /// Gets or sets the personal identifier.
        /// </summary>
        public Guid PersonalEventId { get; set; }

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