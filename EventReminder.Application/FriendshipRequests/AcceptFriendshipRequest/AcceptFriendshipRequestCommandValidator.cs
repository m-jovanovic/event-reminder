using EventReminder.Application.Core.Errors;
using EventReminder.Application.Core.Extensions;
using FluentValidation;

namespace EventReminder.Application.FriendshipRequests.AcceptFriendshipRequest
{
    /// <summary>
    /// Represents the <see cref="AcceptFriendshipRequestCommand"/> validator.
    /// </summary>
    public sealed class AcceptFriendshipRequestCommandValidator : AbstractValidator<AcceptFriendshipRequestCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptFriendshipRequestCommandValidator"/> class.
        /// </summary>
        public AcceptFriendshipRequestCommandValidator() =>
            RuleFor(x => x.FriendshipRequestId)
                .NotEmpty()
                .WithError(ValidationErrors.AcceptFriendshipRequest.FriendshipRequestIdIsRequired);
    }
}
