namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the group event name changed email.
    /// </summary>
    public sealed class GroupEventNameChangedEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventNameChangedEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="oldEventName">The old event name.</param>
        /// <param name="eventDateAndTime">The date and time of the event.</param>
        public GroupEventNameChangedEmail(string emailTo, string name, string eventName, string oldEventName, string eventDateAndTime)
        {
            EmailTo = emailTo;
            Name = name;
            EventName = eventName;
            OldEventName = oldEventName;
            EventDateAndTime = eventDateAndTime;
        }

        /// <summary>
        /// Gets the email receiver.
        /// </summary>
        public string EmailTo { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the event name.
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// Gets the old event name.
        /// </summary>
        public string OldEventName { get; }

        /// <summary>
        /// Gets the date and time of the event.
        /// </summary>
        public string EventDateAndTime { get; }
    }
}
