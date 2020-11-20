using MediatR;

namespace EventReminder.Domain.Core.Events
{
    /// <summary>
    /// Represents a domain event handler interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">The domain event type.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
    }
}