using System;
using EventReminder.Domain.Core.Abstractions;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Core.Utility;
using EventReminder.Domain.Events;

namespace EventReminder.Domain.Entities
{
    /// <summary>
    /// Represents the friendship request.
    /// </summary>
    public sealed class FriendshipRequest : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequest"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="friend">The friend.</param>
        public FriendshipRequest(User user, User friend)
            : base(Guid.NewGuid())
        {
            Ensure.NotNull(user, "The user is required.", nameof(user));
            Ensure.NotEmpty(user.Id, "The user identifier is required.", $"{nameof(user)}{nameof(user.Id)}");
            Ensure.NotNull(friend, "The friend is required.", nameof(friend));
            Ensure.NotEmpty(friend.Id, "The friend identifier is required.", $"{nameof(friend)}{nameof(friend.Id)}");

            UserId = user.Id;
            FriendId = friend.Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequest"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private FriendshipRequest()
        {
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the friend identifier.
        /// </summary>
        public Guid FriendId { get; private set; }

        /// <summary>
        /// Gets the value indicating whether or not the friend request has been accepted.
        /// </summary>
        public bool Accepted { get; private set; }

        /// <summary>
        /// Gets the value indicating whether or not the friend request has been rejected.
        /// </summary>
        public bool Rejected { get; private set; }

        /// <summary>
        /// Gets the date and time the friend request was completed on in UTC format.
        /// </summary>
        public DateTime? CompletedOnUtc { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        /// <summary>
        /// Accepts the friend request.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The result of the accepting operation.</returns>
        public Result Accept(DateTime utcNow)
        {
            if (Accepted)
            {
                return Result.Failure(DomainErrors.FriendshipRequest.AlreadyAccepted);
            }

            if (Rejected)
            {
                return Result.Failure(DomainErrors.FriendshipRequest.AlreadyRejected);
            }

            Accepted = true;

            CompletedOnUtc = utcNow;

            AddDomainEvent(new FriendshipRequestAcceptedDomainEvent(this));

            return Result.Success();
        }

        /// <summary>
        /// Rejects the friend request.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The result of the rejecting operation.</returns>
        public Result Reject(DateTime utcNow)
        {
            if (Accepted)
            {
                return Result.Failure(DomainErrors.FriendshipRequest.AlreadyAccepted);
            }

            if (Rejected)
            {
                return Result.Failure(DomainErrors.FriendshipRequest.AlreadyRejected);
            }

            Rejected = true;

            CompletedOnUtc = utcNow;

            AddDomainEvent(new FriendshipRequestRejectedDomainEvent(this));

            return Result.Success();
        }
    }
}
