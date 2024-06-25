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

namespace EventReminder.Application.Invitations.RejectInvitation
{
    /// <summary>
    /// Represents the <see cref="RejectInvitationCommand"/> class.
    /// </summary>
    internal sealed class RejectInvitationCommandHandler : ICommandHandler<RejectInvitationCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RejectInvitationCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="invitationRepository">The invitation repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dateTime">The date and time.</param>
        public RejectInvitationCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IInvitationRepository invitationRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _invitationRepository = invitationRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(RejectInvitationCommand request, CancellationToken cancellationToken)
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

            Result rejectResult = invitation.Reject(_dateTime.UtcNow);

            if (rejectResult.IsFailure)
            {
                return Result.Failure(rejectResult.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
