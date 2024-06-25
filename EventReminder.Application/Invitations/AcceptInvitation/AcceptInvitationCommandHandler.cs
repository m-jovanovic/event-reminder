using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Authentication;
using EventReminder.Application.Abstractions.Common;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Invitations;
using MediatR;

namespace EventReminder.Application.Invitations.AcceptInvitation
{
    /// <summary>
    /// Represents the <see cref="AcceptInvitationCommand"/> class.
    /// </summary>
    internal sealed class AcceptInvitationCommandHandler : ICommandHandler<AcceptInvitationCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptInvitationCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="invitationRepository">The invitation repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="mediator">The mediator.</param>
        public AcceptInvitationCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IInvitationRepository invitationRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IMediator mediator)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _invitationRepository = invitationRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            Maybe<Invitation> maybeInvitation = await _invitationRepository.GetByIdAsync(request.InvitationId);

            if (maybeInvitation.HasNoValue)
            {
                return Result.Failure(DomainErrors.Invitation.NotFound);
            }

            Invitation invitation = maybeInvitation.Value;

            if (invitation.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.User.InvalidPermissions);
            }

            Result result = invitation.Accept(_dateTime.UtcNow);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
