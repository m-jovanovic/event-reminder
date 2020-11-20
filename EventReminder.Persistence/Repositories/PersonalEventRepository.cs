using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;
using EventReminder.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Persistence.Repositories
{
    /// <summary>
    /// Represents the attendee repository.
    /// </summary>
    internal sealed class PersonalEventRepository : GenericRepository<PersonalEvent>, IPersonalEventRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEventRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PersonalEventRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<PersonalEvent>> GetUnprocessedAsync(int take) =>
            await DbContext.Set<PersonalEvent>()
                .Where(new UnProcessedPersonalEventSpecification())
                .OrderBy(personalEvent => personalEvent.CreatedOnUtc)
                .Take(take)
                .ToArrayAsync();
    }
}
