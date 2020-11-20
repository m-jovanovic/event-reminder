using System;

namespace EventReminder.Contracts.Users
{
    /// <summary>
    /// Represents the user response.
    /// </summary>
    public sealed class UserResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the created on date and time in UTC format.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the number of personal events.
        /// </summary>
        public int NumberOfPersonalEvents { get; set; }

        /// <summary>
        /// Gets or sets the number of friends.
        /// </summary>
        public int NumberOfFriends { get; set; }
    }
}
