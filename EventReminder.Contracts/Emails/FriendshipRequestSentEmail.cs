namespace EventReminder.Contracts.Emails
{
    /// <summary>
    /// Represents the friendship request sent email.
    /// </summary>
    public sealed class FriendshipRequestSentEmail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequestSentEmail"/> class.
        /// </summary>
        /// <param name="emailTo">The email receiver.</param>
        /// <param name="name">The name.</param>
        /// <param name="userWhoSentRequest">THe name of the user who sent the friendship request.</param>
        public FriendshipRequestSentEmail(string emailTo, string name, string userWhoSentRequest)
        {
            EmailTo = emailTo;
            Name = name;
            UserWhoSentRequest = userWhoSentRequest;
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
        /// Gets the name of the user who sent the friendship request.
        /// </summary>
        public string UserWhoSentRequest { get; }
    }
}
