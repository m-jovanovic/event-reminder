using EventReminder.Application.Core.Errors;
using EventReminder.Application.Core.Extensions;
using FluentValidation;

namespace EventReminder.Application.Invitations.AcceptInvitation
{
    /// <summary>
    /// Represents the <see cref="AcceptInvitationCommand"/> validator.
    /// </summary>
    public sealed class AcceptInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptInvitationCommandValidator"/> class.
        /// </summary>
        public AcceptInvitationCommandValidator() =>
            RuleFor(x => x.InvitationId).NotEmpty().WithError(ValidationErrors.AcceptInvitation.InvitationIdIsRequired);
    }
}
