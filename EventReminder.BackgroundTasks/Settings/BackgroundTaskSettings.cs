namespace EventReminder.BackgroundTasks.Settings
{
    /// <summary>
    /// Represents the background task settings.
    /// </summary>
    public class BackgroundTaskSettings
    {
        public const string SettingsKey = "BackgroundTasks";

        /// <summary>
        /// Gets or sets the allowed notification time discrepancy in minutes.
        /// </summary>
        public int AllowedNotificationTimeDiscrepancyInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the attendees batch size.
        /// </summary>
        public int AttendeesBatchSize { get; set; }

        /// <summary>
        /// Gets or sets the personal events batch size.
        /// </summary>
        public int PersonalEventsBatchSize { get; set; }

        /// <summary>
        /// Gets or sets the notifications batch size.
        /// </summary>
        public int NotificationsBatchSize { get; set; }

        /// <summary>
        /// Gets or sets the sleep time in milliseconds.
        /// </summary>
        public int SleepTimeInMilliseconds { get; set; }
    }
}
