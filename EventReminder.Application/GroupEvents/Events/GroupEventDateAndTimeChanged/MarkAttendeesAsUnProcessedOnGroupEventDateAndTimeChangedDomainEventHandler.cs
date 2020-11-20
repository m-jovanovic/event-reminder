using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;
using EventReminder.Domain.Repositories;

namespace EventReminder.Application.GroupEvents.Events.GroupEventDateAndTimeChanged
{
    /// <summary>
    /// Represents the <see cref="GroupEventDateAndTimeChangedDomainEvent"/> class.
    /// </summary>
    internal sealed class MarkAttendeesAsUnProcessedOnGroupEventDateAndTimeChangedDomainEventHandler
        : IDomainEventHandler<GroupEventDateAndTimeChangedDomainEvent>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkAttendeesAsUnProcessedOnGroupEventDateAndTimeChangedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The attendee repository..</param>
        /// <param name="dateTime">The date and time.</param>
        public MarkAttendeesAsUnProcessedOnGroupEventDateAndTimeChangedDomainEventHandler(
            IAttendeeRepository attendeeRepository,
            IDateTime dateTime)
        {
            _attendeeRepository = attendeeRepository;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task Handle(GroupEventDateAndTimeChangedDomainEvent notification, CancellationToken cancellationToken) =>
            await _attendeeRepository.MarkUnprocessedForGroupEventAsync(notification.GroupEvent, _dateTime.UtcNow);
    }
}
