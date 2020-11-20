using System;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Repositories
{
    /// <summary>
    /// Represents the invitation repository interface.
    /// </summary>
    public interface IInvitationRepository
    {
        /// <summary>
        /// Gets the invitation with the specified identifier.
        /// </summary>
        /// <param name="invitationId">The invitation identifier.</param>
        /// <returns>The maybe instance that may contain the invitation with the specified identifier.</returns>
        Task<Maybe<Invitation>> GetByIdAsync(Guid invitationId);

        /// <summary>
        /// Checks if an invitation for the specified event has already been sent.
        /// </summary>
        /// <param name="groupEvent">The event.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<bool> CheckIfInvitationAlreadySentAsync(GroupEvent groupEvent, User user);

        /// <summary>
        /// Inserts the specified invitation to the database.
        /// </summary>
        /// <param name="invitation">The invitation to be inserted to the database.</param>
        void Insert(Invitation invitation);

        /// <summary>
        /// Removes all of the pending invitations for the specified friendship.
        /// </summary>
        /// <param name="friendship">The friendship.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The completed task.</returns>
        Task RemovePendingInvitationsForFriendshipAsync(Friendship friendship, DateTime utcNow);

        /// <summary>
        /// Removes all of the invitations for the specified group event.
        /// </summary>
        /// <param name="groupEvent">The group event.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The completed task.</returns>
        Task RemoveInvitationsForGroupEventAsync(GroupEvent groupEvent, DateTime utcNow);
    }
}
