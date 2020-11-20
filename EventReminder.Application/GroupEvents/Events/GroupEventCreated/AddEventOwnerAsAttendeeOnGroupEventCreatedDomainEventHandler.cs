using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Events;
using EventReminder.Domain.Repositories;

namespace EventReminder.Application.GroupEvents.Events.GroupEventCreated
{
    /// <summary>
    /// Represents the <see cref="GroupEventCreatedDomainEvent"/> handler.
    /// </summary>
    internal sealed class AddEventOwnerAsAttendeeOnGroupEventCreatedDomainEventHandler : IDomainEventHandler<GroupEventCreatedDomainEvent>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddEventOwnerAsAttendeeOnGroupEventCreatedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The attendee repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public AddEventOwnerAsAttendeeOnGroupEventCreatedDomainEventHandler(
            IAttendeeRepository attendeeRepository,
            IUnitOfWork unitOfWork)
        {
            _attendeeRepository = attendeeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task Handle(GroupEventCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Move this into the domain.
            Attendee attendee = notification.GroupEvent.GetOwner();

            _attendeeRepository.Insert(attendee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
