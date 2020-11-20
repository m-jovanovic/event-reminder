namespace EventReminder.Infrastructure.Messaging.Settings
{
    /// <summary>
    /// Represents the message broker settings.
    /// </summary>
    public sealed class MessageBrokerSettings
    {
        public const string SettingsKey = "MessageBroker";

        /// <summary>
        /// Gets or sets the host name.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the queue name.
        /// </summary>
        public string QueueName { get; set; }
    }
}
