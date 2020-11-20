namespace EventReminder.Infrastructure.Emails.Settings
{
    /// <summary>
    /// Represents the mail settings.
    /// </summary>
    public class MailSettings
    {
        public const string SettingsKey = "Mail";

        /// <summary>
        /// Gets or sets the email sender display name.
        /// </summary>
        public string SenderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email sender.
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the SMTP password.
        /// </summary>
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Gets or sets the SMTP server.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Gets or sets the SMTP port.
        /// </summary>
        public int SmtpPort { get; set; }
    }
}
