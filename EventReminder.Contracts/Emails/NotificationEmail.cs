namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the notification email.
    /// </summary>
    public sealed class NotificationEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        public NotificationEmail(string emailTo, string subject, string body)
        {
            EmailTo = emailTo;
            Subject = subject;
            Body = body;
        }

        /// <summary>
        /// Gets the email receiver.
        /// </summary>
        public string EmailTo { get; }
        
        /// <summary>
        /// Gets the email subject.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Gets the email body.
        /// </summary>
        public string Body { get; }
    }
}
