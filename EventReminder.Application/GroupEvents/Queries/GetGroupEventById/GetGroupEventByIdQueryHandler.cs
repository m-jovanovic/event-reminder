using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.GroupEvents;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.GroupEvents.Queries.GetGroupEventById
{
    /// <summary>
    /// Represents the <see cref="GetGroupEventByIdQuery"/> handler.
    /// </summary>
    internal sealed class GetGroupEventByIdQueryHandler : IQueryHandler<GetGroupEventByIdQuery, Maybe<DetailedGroupEventResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGroupEventByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        public GetGroupEventByIdQueryHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        /// <inheritdoc />
        public async Task<Maybe<DetailedGroupEventResponse>> Handle(GetGroupEventByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.GroupEventId == Guid.Empty || !await HasPermissionsToQueryGroupEvent(request.GroupEventId))
            {
                return Maybe<DetailedGroupEventResponse>.None;
            }

            DetailedGroupEventResponse response = await (
                    from groupEvent in _dbContext.Set<GroupEvent>().AsNoTracking()
                    join user in _dbContext.Set<User>().AsNoTracking()
                        on groupEvent.UserId equals user.Id
                    where groupEvent.Id == request.GroupEventId && !groupEvent.Cancelled
                    select new DetailedGroupEventResponse
                    {
                        Id = groupEvent.Id,
                        Name = groupEvent.Name.Value,
                        CategoryId = groupEvent.Category.Value,
                        CreatedBy = user.FirstName.Value + " " + user.LastName.Value,
                        DateTimeUtc = groupEvent.DateTimeUtc,
                        CreatedOnUtc = groupEvent.CreatedOnUtc
                    }).FirstOrDefaultAsync(cancellationToken);

            if (response is null)
            {
                return Maybe<DetailedGroupEventResponse>.None;
            }

            response.Category = Category.FromValue(response.CategoryId).Value.Name;

            response.NumberOfAttendees = await _dbContext.Set<Attendee>()
                .Where(x => x.EventId == response.Id)
                .CountAsync(cancellationToken);

            return response;
        }

        /// <summary>
        /// Checks if the current user has permissions to query this group event.
        /// </summary>
        /// <param name="groupEventId">The group event identifier.</param>
        /// <returns>True if the current user is the group event owner or an attendee.</returns>
        private async Task<bool> HasPermissionsToQueryGroupEvent(Guid groupEventId) =>
            await (
                from attendee in _dbContext.Set<Attendee>().AsNoTracking()
                join groupEvent in _dbContext.Set<GroupEvent>().AsNoTracking()
                    on attendee.EventId equals groupEvent.Id
                where groupEvent.Id == groupEventId &&
                      !groupEvent.Cancelled &&
                      (groupEvent.UserId == _userIdentifierProvider.UserId ||
                       attendee.UserId == _userIdentifierProvider.UserId)
                select true).AnyAsync();
    }
}
