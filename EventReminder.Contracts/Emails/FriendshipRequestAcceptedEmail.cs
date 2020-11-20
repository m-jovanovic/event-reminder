namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the friendship request accepted email.
    /// </summary>
    public sealed class FriendshipRequestAcceptedEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvitationAcceptedEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        /// <param name="friendName">The friend name.</param>
        public FriendshipRequestAcceptedEmail(string emailTo, string name, string friendName)
        {
            EmailTo = emailTo;
            Name = name;
            FriendName = friendName;
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
    }
}
