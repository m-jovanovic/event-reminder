using MediatR;

namespace EventReminder.Application.Core.Abstractions.Messaging
{
    /// <summary>
    /// Represents the event handler interface.
    /// </summary>
    /// <typeparam name="TEvent">The event type.</typeparam>
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : INotification
    {
    }
}