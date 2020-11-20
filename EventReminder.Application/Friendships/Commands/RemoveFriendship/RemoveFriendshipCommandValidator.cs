using EventReminder.Application.Core.Errors;
using EventReminder.Application.Core.Extensions;
using FluentValidation;

namespace EventReminder.Application.Friendships.Commands.RemoveFriendship
{
    /// <summary>
    /// Represents the <see cref="RemoveFriendshipCommand"/> validator.
    /// </summary>
    public sealed class RemoveFriendshipCommandValidator : AbstractValidator<RemoveFriendshipCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveFriendshipCommandValidator"/> class.
        /// </summary>
        public RemoveFriendshipCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithError(ValidationErrors.RemoveFriendship.UserIdIsRequired);

            RuleFor(x => x.FriendId).NotEmpty().WithError(ValidationErrors.RemoveFriendship.FriendIdIsRequired);
        }
    }
}
