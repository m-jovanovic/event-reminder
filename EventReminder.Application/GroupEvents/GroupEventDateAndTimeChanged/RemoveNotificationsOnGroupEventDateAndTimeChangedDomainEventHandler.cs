using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Common;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events.DomainEvents;
using EventReminder.Domain.Notifications;

namespace EventReminder.Application.GroupEvents.GroupEventDateAndTimeChanged
{
    /// <summary>
    /// Represents the <see cref="GroupEventDateAndTimeChangedDomainEvent"/> class.
    /// </summary>
    internal sealed class RemoveNotificationsOnGroupEventDateAndTimeChangedDomainEventHandler
        : IDomainEventHandler<GroupEventDateAndTimeChangedDomainEvent>
    {
        private readonly INotificationRepository _dbContext;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveNotificationsOnGroupEventDateAndTimeChangedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The notification repository.</param>
        /// <param name="dateTime">The date and time.</param>
        public RemoveNotificationsOnGroupEventDateAndTimeChangedDomainEventHandler(INotificationRepository dbContext, IDateTime dateTime)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task Handle(GroupEventDateAndTimeChangedDomainEvent notification, CancellationToken cancellationToken) =>
            await _dbContext.RemoveNotificationsForEventAsync(notification.GroupEvent, _dateTime.UtcNow);
    }
}
