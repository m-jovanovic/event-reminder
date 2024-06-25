using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Authentication;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.Attendees;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Events;
using EventReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.Attendees.GetAttendeesForEventId
{
    /// <summary>
    /// Represents the <see cref="GetAttendeesForGroupEventIdQuery"/> handler.
    /// </summary>
    internal sealed class GetAttendeesForGroupEventIdQueryHandler : IQueryHandler<GetAttendeesForGroupEventIdQuery, Maybe<AttendeeListResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttendeesForGroupEventIdQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        public GetAttendeesForGroupEventIdQueryHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        /// <inheritdoc />
        public async Task<Maybe<AttendeeListResponse>> Handle(GetAttendeesForGroupEventIdQuery request, CancellationToken cancellationToken)
        {
            if (request.GroupEventId == Guid.Empty || !await HasPermissionsToQueryAttendees(request.GroupEventId))
            {
                return Maybe<AttendeeListResponse>.None;
            }

            AttendeeListResponse.AttendeeModel[] attendeeModels = await (
                    from attendee in _dbContext.Set<Attendee>().AsNoTracking()
                    join groupEvent in _dbContext.Set<GroupEvent>().AsNoTracking()
                        on attendee.EventId equals groupEvent.Id
                    join user in _dbContext.Set<User>().AsNoTracking()
                        on attendee.UserId equals user.Id
                    where groupEvent.Id == request.GroupEventId && !groupEvent.Cancelled
                    select new AttendeeListResponse.AttendeeModel
                    {
                        UserId = attendee.UserId,
                        Name = user.FirstName.Value + " " + user.LastName.Value,
                        CreatedOnUtc = attendee.CreatedOnUtc
                    }).ToArrayAsync(cancellationToken);

            var response = new AttendeeListResponse(attendeeModels);

            return response;
        }

        /// <summary>
        /// Checks if the current user has permissions to query attendees for this group event.
        /// </summary>
        /// <param name="groupEventId">The group event identifier.</param>
        /// <returns>True if the current user is the group event owner or an attendee.</returns>
        private async Task<bool> HasPermissionsToQueryAttendees(Guid groupEventId) =>
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
