namespace EventReminder.Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents the integration event publisher interface.
    /// </summary>
    public interface IIntegrationEventPublisher
    {
        /// <summary>
        /// Publishes the specified integration event to the message queue.
        /// </summary>
        /// <param name="integrationEvent">The integration event.</param>
        /// <returns>The completed task.</returns>
        void Publish(IIntegrationEvent integrationEvent);
    }
}
