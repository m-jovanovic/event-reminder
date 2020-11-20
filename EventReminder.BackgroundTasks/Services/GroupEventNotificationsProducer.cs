using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Enumerations;
using EventReminder.Domain.Repositories;

namespace EventReminder.BackgroundTasks.Services
{
    /// <summary>
    /// Represents the group event notifications producer.
    /// </summary>
    internal sealed class GroupEventNotificationsProducer : IGroupEventNotificationsProducer
    {
        private readonly IGroupEventRepository _groupEventRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IDateTime _dateTime;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventNotificationsProducer"/> class.
        /// </summary>
        /// <param name="groupEventRepository">The group event repository.</param>
        /// <param name="attendeeRepository">The attendee repository.</param>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public GroupEventNotificationsProducer(
            IGroupEventRepository groupEventRepository,
            IAttendeeRepository attendeeRepository,
            INotificationRepository notificationRepository,
            IDateTime dateTime,
            IUnitOfWork unitOfWork)
        {
            _groupEventRepository = groupEventRepository;
            _attendeeRepository = attendeeRepository;
            _notificationRepository = notificationRepository;
            _dateTime = dateTime;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task ProduceAsync(int batchSize, CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<Attendee> unprocessedAttendees = await _attendeeRepository.GetUnprocessedAsync(batchSize);

            if (!unprocessedAttendees.Any())
            {
                return;
            }

            IReadOnlyCollection<GroupEvent> groupEvents = await _groupEventRepository.GetForAttendeesAsync(unprocessedAttendees);

            if (!groupEvents.Any())
            {
                return;
            }

            Dictionary<Guid, GroupEvent> groupEventsDictionary = groupEvents.ToDictionary(g => g.Id, g => g);

            var notifications = new List<Notification>();

            foreach (Attendee attendee in unprocessedAttendees)
            {
                Result result = attendee.MarkAsProcessed();

                if (result.IsFailure)
                {
                    continue;
                }

                GroupEvent groupEvent = groupEventsDictionary[attendee.EventId];

                List<Notification> notificationsForAttendee = NotificationType
                    .List
                    .Select(notificationType => notificationType.TryCreateNotification(groupEvent, attendee.UserId, _dateTime.UtcNow))
                    .Where(maybeNotification => maybeNotification.HasValue)
                    .Select(maybeNotification => maybeNotification.Value)
                    .ToList();
                
                notifications.AddRange(notificationsForAttendee);
            }

            _notificationRepository.InsertRange(notifications);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}