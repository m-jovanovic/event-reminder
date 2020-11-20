namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the welcome email.
    /// </summary>
    public sealed class WelcomeEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        public WelcomeEmail(string emailTo, string name)
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
