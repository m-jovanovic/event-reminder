using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Repositories;
using EventReminder.Domain.Services;

namespace EventReminder.Application.Friendships.Commands.RemoveFriendship
{
    /// <summary>
    /// Represents the <see cref="RemoveFriendshipCommand"/> handler.
    /// </summary>
    internal sealed class RemoveFriendshipCommandHandler : ICommandHandler<RemoveFriendshipCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUserRepository _userRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveFriendshipCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="friendshipRepository">The friendship repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public RemoveFriendshipCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IUserRepository userRepository,
            IFriendshipRepository friendshipRepository,
            IUnitOfWork unitOfWork)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _userRepository = userRepository;
            _friendshipRepository = friendshipRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(RemoveFriendshipCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userIdentifierProvider.UserId)
            {
                return Result.Failure(DomainErrors.User.InvalidPermissions);
            }

            var friendshipService = new FriendshipService(_userRepository, _friendshipRepository);

            Result result = await friendshipService.RemoveFriendshipAsync(request.UserId, request.FriendId);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}
