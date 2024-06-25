using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Authentication;
using EventReminder.Application.Abstractions.Common;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Events;

namespace EventReminder.Application.GroupEvents.UpdateGroupEvent
{
    /// <summary>
    /// Represents the <see cref="UpdateGroupEventCommand"/> handler.
    /// </summary>
    internal sealed class UpdateGroupEventCommandHandler : ICommandHandler<UpdateGroupEventCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IGroupEventRepository _groupEventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGroupEventCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="groupEventRepository">The group event repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dateTime">The date and time.</param>
        public UpdateGroupEventCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IGroupEventRepository groupEventRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _groupEventRepository = groupEventRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(UpdateGroupEventCommand request, CancellationToken cancellationToken)
        {
            if (request.DateTimeUtc <= _dateTime.UtcNow)
            {
                return Result.Failure(DomainErrors.GroupEvent.DateAndTimeIsInThePast);
            }

            Maybe<GroupEvent> maybeGroupEvent = await _groupEventRepository.GetByIdAsync(request.GroupEventId);

            if (maybeGroupEvent.HasNoValue)
            {
                return Result.Failure(DomainErrors.GroupEvent.NotFound);
            }

            GroupEvent groupEvent = maybeGroupEvent.Value;

            if (groupEvent.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.User.InvalidPermissions);
            }

            Result<Name> nameResult = Name.Create(request.Name);

            if (nameResult.IsFailure)
            {
                return Result.Failure(nameResult.Error);
            }

            groupEvent.ChangeName(nameResult.Value);

            groupEvent.ChangeDateAndTime(request.DateTimeUtc);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
