using MediatR;

namespace EventReminder.Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents the event interface.
    /// </summary>
    public interface IEvent : INotification
    {
    }
}