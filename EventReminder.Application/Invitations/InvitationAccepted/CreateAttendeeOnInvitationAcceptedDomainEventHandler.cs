using EventReminder.Application.Abstractions.Data;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;
using EventReminder.Domain.Invitations.DomainEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Application.Attendees.AttendeeCreated;

namespace EventReminder.Application.Invitations.InvitationAccepted
{
    /// <summary>
    /// Represents the <see cref="InvitationAcceptedDomainEvent"/> handler.
    /// </summary>
    internal sealed class CreateAttendeeOnInvitationAcceptedDomainEventHandler : IDomainEventHandler<InvitationAcceptedDomainEvent>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttendeeOnInvitationAcceptedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The attendee repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mediator">The mediator.</param>
        public CreateAttendeeOnInvitationAcceptedDomainEventHandler(
            IAttendeeRepository attendeeRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _attendeeRepository = attendeeRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Move this into the domain somehow.
            var attendee = new Attendee(notification.Invitation);

            _attendeeRepository.Insert(attendee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AttendeeCreatedEvent(attendee.Id), cancellationToken);
        }
    }
}
