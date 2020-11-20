namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the mail request.
    /// </summary>
    public sealed class MailRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailRequest"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public MailRequest(string emailTo, string subject, string body)
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
        /// Gets the subject.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Gets the body.
        /// </summary>
        public string Body { get; }
    }
}
