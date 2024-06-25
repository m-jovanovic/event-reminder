using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Common;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Events;
using EventReminder.Domain.Notifications;

namespace EventReminder.BackgroundTasks.Services
{
    /// <summary>
    /// Represents the personal event notifications producer.
    /// </summary>
    internal sealed class PersonalEventNotificationsProducer : IPersonalEventNotificationsProducer
    {
        private readonly IPersonalEventRepository _personalEventRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IDateTime _dateTime;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEventNotificationsProducer"/> class.
        /// </summary>
        /// <param name="personalEventRepository">The personal event repository.</param>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public PersonalEventNotificationsProducer(
            IPersonalEventRepository personalEventRepository,
            INotificationRepository notificationRepository,
            IDateTime dateTime,
            IUnitOfWork unitOfWork)
        {
            _personalEventRepository = personalEventRepository;
            _notificationRepository = notificationRepository;
            _dateTime = dateTime;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task ProduceAsync(int batchSize, CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<PersonalEvent> unprocessedPersonalEvents = await _personalEventRepository.GetUnprocessedAsync(batchSize);

            if (!unprocessedPersonalEvents.Any())
            {
                return;
            }

            var notifications = new List<Notification>();

            foreach (PersonalEvent personalEvent in unprocessedPersonalEvents)
            {
                Result result = personalEvent.MarkAsProcessed();

                if (result.IsFailure)
                {
                    continue;
                }

                List<Notification> notificationsForPersonalEvent = NotificationType
                    .List
                    .Select(notificationType => notificationType.TryCreateNotification(personalEvent, personalEvent.UserId, _dateTime.UtcNow))
                    .Where(maybeNotification => maybeNotification.HasValue)
                    .Select(maybeNotification => maybeNotification.Value)
                    .ToList();

                notifications.AddRange(notificationsForPersonalEvent);
            }

            _notificationRepository.InsertRange(notifications);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}