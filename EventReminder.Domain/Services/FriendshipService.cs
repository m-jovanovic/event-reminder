using System;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;

namespace EventReminder.Domain.Services
{
    /// <summary>
    /// Represents the friendship service.
    /// </summary>
    public sealed class FriendshipService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendshipRepository _friendshipRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="friendshipRepository">The friendship repository.</param>
        public FriendshipService(IUserRepository userRepository, IFriendshipRepository friendshipRepository)
        {
            _userRepository = userRepository;
            _friendshipRepository = friendshipRepository;
        }

        /// <summary>
        /// Creates the friendship based on the specified 
        /// </summary>
        /// <param name="friendshipRequest"></param>
        /// <returns></returns>
        public async Task CreateFriendshipAsync(FriendshipRequest friendshipRequest)
        {
            if (friendshipRequest.Rejected)
            {
                throw new DomainException(DomainErrors.FriendshipRequest.AlreadyRejected);
            }

            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(friendshipRequest.UserId);

            if (maybeUser.HasNoValue)
            {
                throw new DomainException(DomainErrors.FriendshipRequest.UserNotFound);
            }

            Maybe<User> maybeFriend = await _userRepository.GetByIdAsync(friendshipRequest.FriendId);

            if (maybeFriend.HasNoValue)
            {
                throw new DomainException(DomainErrors.FriendshipRequest.FriendNotFound);
            }

            User user = maybeUser.Value;

            User friend = maybeFriend.Value;

            _friendshipRepository.Insert(new Friendship(user, friend));

            _friendshipRepository.Insert(new Friendship(friend, user));
        }

        /// <summary>
        /// Removes the friendship between the specified users.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>The result of the operation.</returns>
        public async Task<Result> RemoveFriendshipAsync(Guid userId, Guid friendId)
        {
            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(userId);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.Friendship.UserNotFound);
            }

            Maybe<User> maybeFriend = await _userRepository.GetByIdAsync(friendId);

            if (maybeFriend.HasNoValue)
            {
                return Result.Failure(DomainErrors.Friendship.FriendNotFound);
            }

            User user = maybeUser.Value;

            User friend = maybeFriend.Value;

            if (!await _friendshipRepository.CheckIfFriendsAsync(user, friend))
            {
                return Result.Failure(DomainErrors.Friendship.NotFriends);
            }

            var userToFriendFriendship = new Friendship(user, friend);

            var friendToUserFriendship = new Friendship(friend, user);

            // This will add the appropriate domain event that will be published after saving the changes.
            user.RemoveFriendship(userToFriendFriendship);

            _friendshipRepository.Remove(userToFriendFriendship);

            _friendshipRepository.Remove(friendToUserFriendship);

            return Result.Success();
        }
    }
}
