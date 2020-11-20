namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the password changed email.
    /// </summary>
    public sealed class PasswordChangedEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordChangedEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        public PasswordChangedEmail(string emailTo, string name)
        {
            EmailTo = emailTo;
            Name = name;
        }

        /// <summary>
        /// Gets the email receiver.
        /// </summary>
        public string EmailTo { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }
    }
}
