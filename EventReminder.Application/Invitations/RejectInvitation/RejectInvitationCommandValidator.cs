using EventReminder.Application.Core.Errors;
using EventReminder.Application.Core.Extensions;
using FluentValidation;

namespace EventReminder.Application.Invitations.RejectInvitation
{
    /// <summary>
    /// Represents the <see cref="RejectInvitationCommand"/> validator.
    /// </summary>
    public sealed class RejectInvitationCommandValidator : AbstractValidator<RejectInvitationCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RejectInvitationCommandValidator"/> class.
        /// </summary>
        public RejectInvitationCommandValidator() =>
            RuleFor(x => x.InvitationId).NotEmpty().WithError(ValidationErrors.RejectInvitation.InvitationIdIsRequired);
    }
}
