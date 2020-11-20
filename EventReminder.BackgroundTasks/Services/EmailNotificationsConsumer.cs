using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Notifications;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;

namespace EventReminder.BackgroundTasks.Services
{
    /// <summary>
    /// Represents the email notifications consumer.
    /// </summary>
    internal sealed class EmailNotificationsConsumer : IEmailNotificationsConsumer
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly IEmailNotificationService _emailNotificationService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationsConsumer"/> class.
        /// </summary>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public EmailNotificationsConsumer(
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IEmailNotificationService emailNotificationService)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
            _emailNotificationService = emailNotificationService;
        }

        /// <inheritdoc />
        public async Task ConsumeAsync(
            int batchSize,
            int allowedNotificationTimeDiscrepancyInMinutes,
            CancellationToken cancellationToken = default)
        {
            (Notification Notification, Event Event, User User)[] notificationsToBeSent =
                await _notificationRepository.GetNotificationsToBeSentIncludingUserAndEvent(
                    batchSize,
                    _dateTime.UtcNow,
                    allowedNotificationTimeDiscrepancyInMinutes);

            var sendNotificationEmailTasks = new List<Task>();

            foreach ((Notification notification, Event @event, User user) in notificationsToBeSent)
            {
                Result result = notification.MarkAsSent();

                if (result.IsFailure)
                {
                    continue;
                }

                _notificationRepository.Update(notification);

                (string subject, string body) = notification.CreateNotificationEmail(@event, user);

                var notificationEmail = new NotificationEmail(user.Email, subject, body);

                sendNotificationEmailTasks.Add(_emailNotificationService.SendNotificationEmail(notificationEmail));
            }

            await Task.WhenAll(sendNotificationEmailTasks);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}