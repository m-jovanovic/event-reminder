using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Enumerations;
using EventReminder.Domain.Repositories;
using EventReminder.Domain.ValueObjects;

namespace EventReminder.Application.PersonalEvents.Commands.CreatePersonalEvent
{
    /// <summary>
    /// Represents the <see cref="CreatePersonalEventCommand"/> handler.
    /// </summary>
    internal sealed class CreatePersonalEventCommandHandler : ICommandHandler<CreatePersonalEventCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUserRepository _userRepository;
        private readonly IPersonalEventRepository _personalEventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePersonalEventCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="personalEventRepository">The personal event repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dateTime">The date and time.</param>
        public CreatePersonalEventCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IUserRepository userRepository,
            IPersonalEventRepository personalEventRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _userRepository = userRepository;
            _personalEventRepository = personalEventRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(CreatePersonalEventCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.User.InvalidPermissions);
            }

            if (request.DateTimeUtc <= _dateTime.UtcNow)
            {
                return Result.Failure(DomainErrors.PersonalEvent.DateAndTimeIsInThePast);
            }

            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.User.NotFound);
            }

            Maybe<Category> maybeCategory = Category.FromValue(request.CategoryId);

            if (maybeCategory.HasNoValue)
            {
                return Result.Failure(DomainErrors.Category.NotFound);
            }

            Result<Name> nameResult = Name.Create(request.Name);

            if (nameResult.IsFailure)
            {
                return Result.Failure(nameResult.Error);
            }

            User user = maybeUser.Value;

            PersonalEvent personalEvent = user.CreatePersonalEvent(nameResult.Value, maybeCategory.Value, request.DateTimeUtc);

            _personalEventRepository.Insert(personalEvent);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
