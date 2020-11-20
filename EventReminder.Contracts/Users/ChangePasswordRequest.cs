using System;

namespace EventReminder.Contracts.Users
{
    /// <summary>
    /// Represents the change password request.
    /// </summary>
    public sealed class ChangePasswordRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        public string Password { get; set; }
    }
}
