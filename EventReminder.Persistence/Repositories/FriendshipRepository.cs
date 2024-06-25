using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Domain.Friendships;
using EventReminder.Domain.Users;
using EventReminder.Persistence.Specifications;

namespace EventReminder.Persistence.Repositories
{
    /// <summary>
    /// Represents the friendship repository.
    /// </summary>
    internal sealed class FriendshipRepository :  GenericRepository<Friendship>,IFriendshipRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public FriendshipRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        /// <inheritdoc />
        public async Task<bool> CheckIfFriendsAsync(User user, User friend) => await AnyAsync(new FriendshipSpecification(user, friend));
    }
}
