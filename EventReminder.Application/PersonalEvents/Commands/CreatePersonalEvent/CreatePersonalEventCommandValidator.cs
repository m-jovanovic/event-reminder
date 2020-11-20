using EventReminder.Application.Core.Errors;
using EventReminder.Application.Core.Extensions;
using FluentValidation;

namespace EventReminder.Application.PersonalEvents.Commands.CreatePersonalEvent
{
    /// <summary>
    /// Represents the <see cref="CreatePersonalEventCommand"/> validator.
    /// </summary>
    public sealed class CreateGroupEventCommandValidator : AbstractValidator<CreatePersonalEventCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGroupEventCommandValidator"/> class.
        /// </summary>
        public CreateGroupEventCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithError(ValidationErrors.CreatePersonalEvent.UserIdIsRequired);

            RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.CreatePersonalEvent.NameIsRequired);

            RuleFor(x => x.CategoryId).NotEmpty().WithError(ValidationErrors.CreatePersonalEvent.CategoryIdIsRequired);

            RuleFor(x => x.DateTimeUtc).NotEmpty().WithError(ValidationErrors.CreatePersonalEvent.DateAndTimeIsRequired);
        }
    }
}
