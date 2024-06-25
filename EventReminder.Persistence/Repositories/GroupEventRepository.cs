using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Domain.Events;
using EventReminder.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Persistence.Repositories
{
    /// <summary>
    /// Represents the group event repository.
    /// </summary>
    internal sealed class GroupEventRepository : GenericRepository<GroupEvent>, IGroupEventRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GroupEventRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<GroupEvent>> GetForAttendeesAsync(IReadOnlyCollection<Attendee> attendees) =>
            attendees.Any()
                ? await DbContext.Set<GroupEvent>().Where(new GroupEventForAttendeesSpecification(attendees)).ToArrayAsync()
                : Array.Empty<GroupEvent>();
    }
}
