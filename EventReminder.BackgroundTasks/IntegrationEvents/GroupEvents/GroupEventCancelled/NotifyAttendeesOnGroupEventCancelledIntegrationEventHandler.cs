using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Notifications;
using EventReminder.Application.GroupEvents.GroupEventCancelled;
using EventReminder.BackgroundTasks.Abstractions.Messaging;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Events;

namespace EventReminder.BackgroundTasks.IntegrationEvents.GroupEvents.GroupEventCancelled
{
    /// <summary>
    /// Represents the <see cref="GroupEventCancelledIntegrationEvent"/> class.
    /// </summary>
    internal sealed class NotifyAttendeesOnGroupEventCancelledIntegrationEventHandler
        : IIntegrationEventHandler<GroupEventCancelledIntegrationEvent>
    {
        private readonly IGroupEventRepository _groupEventRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyAttendeesOnGroupEventCancelledIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="groupEventRepository">The group event repository.</param>
        /// <param name="attendeeRepository">The attendee repository.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public NotifyAttendeesOnGroupEventCancelledIntegrationEventHandler(
            IGroupEventRepository groupEventRepository,
            IAttendeeRepository attendeeRepository,
            IEmailNotificationService emailNotificationService)
        {
            _groupEventRepository = groupEventRepository;
            _attendeeRepository = attendeeRepository;
            _emailNotificationService = emailNotificationService;
        }

        /// <inheritdoc />
        public async Task Handle(GroupEventCancelledIntegrationEvent notification, CancellationToken cancellationToken)
        {
            Maybe<GroupEvent> maybeGroupEvent = await _groupEventRepository.GetByIdAsync(notification.GroupEventId);

            if (maybeGroupEvent.HasNoValue)
            {
                throw new DomainException(DomainErrors.GroupEvent.NotFound);
            }

            GroupEvent groupEvent = maybeGroupEvent.Value;

            (string Email, string Name)[] attendeeEmailsAndNames = await _attendeeRepository.GetEmailsAndNamesForGroupEvent(groupEvent);

            if (attendeeEmailsAndNames.Length == 0)
            {
                return;
            }

            IEnumerable<Task> sendGroupEventCancelledEmailTasks = attendeeEmailsAndNames
                .Select(emailAndName =>
                    new GroupEventCancelledEmail(
                        emailAndName.Email,
                        emailAndName.Name,
                        groupEvent.Name,
                        groupEvent.DateTimeUtc.ToString(CultureInfo.InvariantCulture)))
                .Select(groupEventCancelledEmail => _emailNotificationService.SendGroupEventCancelledEmail(groupEventCancelledEmail));

            await Task.WhenAll(sendGroupEventCancelledEmailTasks);
        }
    }
}
