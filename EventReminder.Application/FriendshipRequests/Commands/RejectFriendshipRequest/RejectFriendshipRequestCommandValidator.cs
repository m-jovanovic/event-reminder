using EventReminder.Application.Core.Errors;
using EventReminder.Application.Core.Extensions;
using FluentValidation;

namespace EventReminder.Application.FriendshipRequests.Commands.RejectFriendshipRequest
{
    /// <summary>
    /// Represents the <see cref="RejectFriendshipRequestCommand"/> validator.
    /// </summary>
    public sealed class RejectFriendshipRequestCommandValidator : AbstractValidator<RejectFriendshipRequestCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RejectFriendshipRequestCommandValidator"/> class.
        /// </summary>
        public RejectFriendshipRequestCommandValidator() =>
            RuleFor(x => x.FriendshipRequestId)
                .NotEmpty()
                .WithError(ValidationErrors.RejectFriendshipRequest.FriendshipRequestIdIsRequired);
    }
}
