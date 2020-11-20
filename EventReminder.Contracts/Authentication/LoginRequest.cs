namespace EventReminder.Contracts.Authentication
{
    /// <summary>
    /// Represents the login request.
    /// </summary>
    public sealed class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }
    }
}
