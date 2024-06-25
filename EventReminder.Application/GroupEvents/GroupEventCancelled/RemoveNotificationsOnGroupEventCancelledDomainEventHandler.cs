using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Common;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events.DomainEvents;
using EventReminder.Domain.Notifications;

namespace EventReminder.Application.GroupEvents.GroupEventCancelled
{
    /// <summary>
    /// Represents the <see cref="GroupEventCancelledDomainEvent"/> class.
    /// </summary>
    internal sealed class RemoveNotificationsOnGroupEventCancelledDomainEventHandler : IDomainEventHandler<GroupEventCancelledDomainEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveNotificationsOnGroupEventCancelledDomainEventHandler"/> class.
        /// </summary>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="dateTime">The date and time.</param>
        public RemoveNotificationsOnGroupEventCancelledDomainEventHandler(INotificationRepository notificationRepository, IDateTime dateTime)
        {
            _notificationRepository = notificationRepository;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task Handle(GroupEventCancelledDomainEvent notification, CancellationToken cancellationToken) =>
            await _notificationRepository.RemoveNotificationsForEventAsync(notification.GroupEvent, _dateTime.UtcNow);
    }
}
