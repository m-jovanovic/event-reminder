using System;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Repositories
{
    /// <summary>
    /// Represents the friendship request repository interface.
    /// </summary>
    public interface IFriendshipRequestRepository
    {
        /// <summary>
        /// Gets the friend request with the specified identifier.
        /// </summary>
        /// <param name="friendshipRequestId">The friendship request identifier.</param>
        /// <returns>The maybe instance that may contain the friendship request with the specified identifier.</returns>
        Task<Maybe<FriendshipRequest>> GetByIdAsync(Guid friendshipRequestId);

        /// <summary>
        /// Checks if the specified users have a pending friendship request.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>True if the specified users have a pending friendship request, otherwise false.</returns>
        Task<bool> CheckForPendingFriendshipRequestAsync(User user, User friend);

        /// <summary>
        /// Inserts the specified friendship request to the database.
        /// </summary>
        /// <param name="friendshipRequest">The friendship request to be inserted to the database.</param>
        void Insert(FriendshipRequest friendshipRequest);
    }
}