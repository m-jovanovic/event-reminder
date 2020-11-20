using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;
using EventReminder.Domain.ValueObjects;

namespace EventReminder.Application.Users.Commands.UpdateUser
{
    /// <summary>
    /// Represents the <see cref="UpdateUserCommand"/> handler.
    /// </summary>
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UpdateUserCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.User.InvalidPermissions);
            }

            Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
            Result<LastName> lastNameResult = LastName.Create(request.LastName);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure(firstFailureOrSuccess.Error);
            }

            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.User.NotFound);
            }

            User user = maybeUser.Value;

            user.ChangeName(firstNameResult.Value, lastNameResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
