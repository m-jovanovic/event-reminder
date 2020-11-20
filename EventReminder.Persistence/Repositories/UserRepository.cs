using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;
using EventReminder.Domain.ValueObjects;
using EventReminder.Persistence.Specifications;

namespace EventReminder.Persistence.Repositories
{
    /// <summary>
    /// Represents the user repository.
    /// </summary>
    internal sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UserRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        /// <inheritdoc />
        public async Task<Maybe<User>> GetByEmailAsync(Email email) => await FirstOrDefaultAsync(new UserWithEmailSpecification(email));

        /// <inheritdoc />
        public async Task<bool> IsEmailUniqueAsync(Email email) => !await AnyAsync(new UserWithEmailSpecification(email));
    }
}
