using System;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Abstractions;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Guards;
using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Events;
using EventReminder.Domain.Events.DomainEvents;
using EventReminder.Domain.Friendships;
using EventReminder.Domain.Friendships.DomainEvents;
using EventReminder.Domain.Users.DomainEvents;

namespace EventReminder.Domain.Users
{
    /// <summary>
    /// Represents the user entity.
    /// </summary>
    public sealed class User : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {
        private string _passwordHash;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="email">The user email instance.</param>
        /// <param name="passwordHash">The user password hash.</param>
        private User(FirstName firstName, LastName lastName, Email email, string passwordHash)
            : base(Guid.NewGuid())
        {
            Ensure.NotEmpty(firstName, "The first name is required.", nameof(firstName));
            Ensure.NotEmpty(lastName, "The last name is required.", nameof(lastName));
            Ensure.NotEmpty(email, "The email is required.", nameof(email));
            Ensure.NotEmpty(passwordHash, "The password hash is required", nameof(passwordHash));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            _passwordHash = passwordHash;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private User()
        {
        }

        /// <summary>
        /// Gets the user first name.
        /// </summary>
        public FirstName FirstName { get; private set; }

        /// <summary>
        /// Gets the user last name.
        /// </summary>
        public LastName LastName { get; private set; }

        /// <summary>
        /// Gets the user full name.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets the user email.
        /// </summary>
        public Email Email { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        /// <summary>
        /// Creates a new user with the specified first name, last name, email and password hash.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns>The newly created user instance.</returns>
        public static User Create(FirstName firstName, LastName lastName, Email email, string passwordHash)
        {
            var user = new User(firstName, lastName, email, passwordHash);

            user.AddDomainEvent(new UserCreatedDomainEvent(user));

            return user;
        }

        /// <summary>
        /// Creates a new friendship request for the specified friend.
        /// </summary>
        /// <param name="friend">The friend.</param>
        /// <param name="friendshipRepository">The friendship repository.</param>
        /// <param name="friendshipRequestRepository">The friendship request repository.</param>
        /// <returns>The result that contains a friendship request or an error.</returns>
        public async Task<Result<FriendshipRequest>> SendFriendshipRequestAsync(
            User friend,
            IFriendshipRepository friendshipRepository,
            IFriendshipRequestRepository friendshipRequestRepository)
        {
            if (await friendshipRepository.CheckIfFriendsAsync(this, friend))
            {
                return Result.Failure<FriendshipRequest>(DomainErrors.FriendshipRequest.AlreadyFriends);
            }

            if (await friendshipRequestRepository.CheckForPendingFriendshipRequestAsync(this, friend))
            {
                return Result.Failure<FriendshipRequest>(DomainErrors.FriendshipRequest.PendingFriendshipRequest);
            }

            var friendshipRequest = new FriendshipRequest(this, friend);

            AddDomainEvent(new FriendshipRequestSentDomainEvent(friendshipRequest));

            return friendshipRequest;
        }

        /// <summary>
        /// Creates a new personal event.
        /// </summary>
        /// <param name="name">The event name.</param>
        /// <param name="category">The event category.</param>
        /// <param name="dateTimeUtc">The date and time of the event.</param>
        /// <returns>The newly created personal event.</returns>
        public PersonalEvent CreatePersonalEvent(Name name, Category category, DateTime dateTimeUtc)
        {
            var personalEvent = new PersonalEvent(this, name, category, dateTimeUtc);

            AddDomainEvent(new PersonalEventCreatedDomainEvent(personalEvent));

            return personalEvent;
        }

        /// <summary>
        /// Verifies that the provided password hash matches the password hash.
        /// </summary>
        /// <param name="password">The password to be checked against the user password hash.</param>
        /// <param name="passwordHashChecker">The password hash checker.</param>
        /// <returns>True if the password hashes match, otherwise false.</returns>
        public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
            => !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(_passwordHash, password);

        /// <summary>
        /// Changes the users password.
        /// </summary>
        /// <param name="passwordHash">The password hash of the new password.</param>
        /// <returns>The success result or an error.</returns>
        public Result ChangePassword(string passwordHash)
        {
            if (passwordHash == _passwordHash)
            {
                return Result.Failure(DomainErrors.User.CannotChangePassword);
            }

            _passwordHash = passwordHash;

            AddDomainEvent(new UserPasswordChangedDomainEvent(this));

            return Result.Success();
        }

        /// <summary>
        /// Changes the users first and last name.
        /// </summary>
        /// <param name="firstName">The new first name.</param>
        /// <param name="lastName">The new last name.</param>
        public void ChangeName(FirstName firstName, LastName lastName)
        {
            Ensure.NotEmpty(firstName, "The first name is required.", nameof(firstName));
            Ensure.NotEmpty(lastName, "The last name is required.", nameof(lastName));

            FirstName = firstName;

            LastName = lastName;

            AddDomainEvent(new UserNameChangedDomainEvent(this));
        }

        /// <summary>
        /// Removes the specified friendship.
        /// </summary>
        /// <param name="friendship">The friendship.</param>
        internal void RemoveFriendship(Friendship friendship) => AddDomainEvent(new FriendshipRemovedDomainEvent(friendship));
    }
}
