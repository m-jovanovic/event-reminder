namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event type.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Personal event.
        /// </summary>
        PersonalEvent = 0,

        /// <summary>
        /// Group event.
        /// </summary>
        GroupEvent = 1
    }
}
