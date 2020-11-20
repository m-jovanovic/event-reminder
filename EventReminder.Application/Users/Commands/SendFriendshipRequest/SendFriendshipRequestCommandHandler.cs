using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;

namespace EventReminder.Application.Users.Commands.SendFriendshipRequest
{
    /// <summary>
    /// Represents the <see cref="SendFriendshipRequestCommand"/> handler.
    /// </summary>
    internal sealed class SendFriendshipRequestCommandHandler : ICommandHandler<SendFriendshipRequestCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUserRepository _userRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendFriendshipRequestCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="friendshipRepository">The friendship repository.</param>
        /// <param name="friendshipRequestRepository">The friendship request repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public SendFriendshipRequestCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IUserRepository userRepository,
            IFriendshipRepository friendshipRepository,
            IFriendshipRequestRepository friendshipRequestRepository,
            IUnitOfWork unitOfWork)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _userRepository = userRepository;
            _friendshipRepository = friendshipRepository;
            _friendshipRequestRepository = friendshipRequestRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(SendFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.User.InvalidPermissions);
            }

            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.User.NotFound);
            }

            Maybe<User> maybeFriend = await _userRepository.GetByIdAsync(request.FriendId);

            if (maybeFriend.HasNoValue)
            {
                return Result.Failure(DomainErrors.FriendshipRequest.FriendNotFound);
            }

            User user = maybeUser.Value;

            Result<FriendshipRequest> friendshipRequestResult = await user.SendFriendshipRequestAsync(
                maybeFriend.Value,
                _friendshipRepository,
                _friendshipRequestRepository);

            if (friendshipRequestResult.IsFailure)
            {
                return Result.Failure(friendshipRequestResult.Error);
            }

            FriendshipRequest friendshipRequest = friendshipRequestResult.Value;

            _friendshipRequestRepository.Insert(friendshipRequest);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
