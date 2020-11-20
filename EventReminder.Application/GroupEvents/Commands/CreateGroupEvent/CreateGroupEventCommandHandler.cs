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
using MediatR;

namespace EventReminder.Application.GroupEvents.Commands.CreateGroupEvent
{
    /// <summary>
    /// Represents the <see cref="CreateGroupEventCommand"/> handler.
    /// </summary>
    internal sealed class CreateGroupEventCommandHandler : ICommandHandler<CreateGroupEventCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUserRepository _userRepository;
        private readonly IGroupEventRepository _groupEventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGroupEventCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="groupEventRepository">The group event repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="mediator">The mediator.</param>
        public CreateGroupEventCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IUserRepository userRepository,
            IGroupEventRepository groupEventRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IMediator mediator)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _userRepository = userRepository;
            _groupEventRepository = groupEventRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
            _mediator = mediator;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(CreateGroupEventCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.User.InvalidPermissions);
            }

            if (request.DateTimeUtc <= _dateTime.UtcNow)
            {
                return Result.Failure(DomainErrors.GroupEvent.DateAndTimeIsInThePast);
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

            var groupEvent = GroupEvent.Create(maybeUser.Value, nameResult.Value, maybeCategory.Value, request.DateTimeUtc);

            _groupEventRepository.Insert(groupEvent);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
