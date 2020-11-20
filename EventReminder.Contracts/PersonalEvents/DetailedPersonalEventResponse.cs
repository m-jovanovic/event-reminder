using System;

namespace EventReminder.Contracts.PersonalEvents
{
    /// <summary>
    /// Represents the personal event response.
    /// </summary>
    public sealed class DetailedPersonalEventResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the created by name.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time in UTC format.
        /// </summary>
        public DateTime DateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the created on date and time in UTC format.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
