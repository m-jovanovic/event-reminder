namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the invitation rejected email.
    /// </summary>
    public sealed class InvitationRejectedEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvitationRejectedEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        /// <param name="friendName">The friend name.</param>
        /// <param name="eventName">The event name.</param>
        /// <param name="eventDateAndTime">The date and time of the event.</param>
        public InvitationRejectedEmail(string emailTo, string name, string friendName, string eventName, string eventDateAndTime)
        {
            EmailTo = emailTo;
            Name = name;
            FriendName = friendName;
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
        /// Gets the friend name.
        /// </summary>
        public string FriendName { get; }

        /// <summary>
        /// Gets the event name.
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// Gets the date and time of the event.
        /// </summary>
        public string EventDateAndTime { get; }
    }
}
