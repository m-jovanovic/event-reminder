namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the group event date and time changed email.
    /// </summary>
    public sealed class GroupEventDateAndTimeChangedEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventDateAndTimeChangedEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="oldEventDateAndTime">The old date and time of the event.</param>
        /// <param name="eventDateAndTime">The date and time of the event.</param>
        public GroupEventDateAndTimeChangedEmail(string emailTo, string name, string eventName, string oldEventDateAndTime, string eventDateAndTime)
        {
            EmailTo = emailTo;
            Name = name;
            EventName = eventName;
            OldEventDateAndTime = oldEventDateAndTime;
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
        /// Gets the old date and time of the event.
        /// </summary>
        public string OldEventDateAndTime { get; }

        /// <summary>
        /// Gets the date and time of the event.
        /// </summary>
        public string EventDateAndTime { get; }
    }
}