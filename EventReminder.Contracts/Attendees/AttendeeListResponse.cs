using System;

namespace EventReminder.Contracts.Attendees
{
    /// <summary>
    /// Represents the attendee list response.
    /// </summary>
    public sealed class AttendeeListResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeListResponse"/> class.
        /// </summary>
        /// <param name="attendees">The attendees.</param>
        public AttendeeListResponse(AttendeeModel[] attendees) => Attendees = attendees;

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        public AttendeeModel[] Attendees { get; }

        /// <summary>
        /// Represents the attendee model.
        /// </summary>
        public sealed class AttendeeModel
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
            /// Gets or sets the created on date and time in UTC format.
            /// </summary>
            public DateTime CreatedOnUtc { get; set; }
        }
    }
}
