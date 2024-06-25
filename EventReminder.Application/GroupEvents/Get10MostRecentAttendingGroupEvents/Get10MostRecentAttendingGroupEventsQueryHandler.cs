using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Authentication;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.GroupEvents;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.GroupEvents.Get10MostRecentAttendingGroupEvents
{
    /// <summary>
    /// Represents the <see cref="Get10MostRecentAttendingGroupEventsQuery"/> handler.
    /// </summary>
    internal sealed class Get10MostRecentAttendingGroupEventsQueryHandler
        : IQueryHandler<Get10MostRecentAttendingGroupEventsQuery, Maybe<IReadOnlyCollection<GroupEventResponse>>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Get10MostRecentAttendingGroupEventsQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public Get10MostRecentAttendingGroupEventsQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<IReadOnlyCollection<GroupEventResponse>>> Handle(
            Get10MostRecentAttendingGroupEventsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty || request.UserId != _userIdentifierProvider.UserId)
            {
                return Maybe<IReadOnlyCollection<GroupEventResponse>>.None;
            }

            GroupEventResponse[] responses = await (
                    from attendee in _dbContext.Set<Attendee>().AsNoTracking()
                    join groupEvent in _dbContext.Set<GroupEvent>().AsNoTracking()
                        on attendee.EventId equals groupEvent.Id
                    where attendee.UserId == request.UserId
                    orderby groupEvent.DateTimeUtc
                    select new GroupEventResponse
                    {
                        Id = groupEvent.Id,
                        Name = groupEvent.Name.Value,
                        CategoryId = groupEvent.Category.Value,
                        DateTimeUtc = groupEvent.DateTimeUtc,
                        CreatedOnUtc = groupEvent.CreatedOnUtc
                    })
                .Take(request.NumberOfGroupEventsToTake)
                .ToArrayAsync(cancellationToken);

            foreach (GroupEventResponse groupEventResponse in responses)
            {
                groupEventResponse.Category = Category.FromValue(groupEventResponse.CategoryId).Value.Name;
            }

            return responses;
        }
    }
}
