namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the attendee created email.
    /// </summary>
    public sealed class AttendeeCreatedEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCreatedEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="eventDateAndTime">The event date and time.</param>
        public AttendeeCreatedEmail(string emailTo, string name, string eventName, string eventDateAndTime)
        {
            EmailTo = emailTo;
            Name = name;
            EventName = eventName;
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
        /// Gets the event date and time.
        /// </summary>
        public string EventDateAndTime { get; }
    }
}
