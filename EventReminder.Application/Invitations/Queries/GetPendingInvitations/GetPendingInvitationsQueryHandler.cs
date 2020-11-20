using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.Invitations;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.Invitations.Queries.GetPendingInvitations
{
    /// <summary>
    /// Represents the <see cref="GetPendingInvitationsQuery"/> handler.
    /// </summary>
    internal sealed class GetPendingInvitationsQueryHandler
        : IQueryHandler<GetPendingInvitationsQuery, Maybe<PendingInvitationsListResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPendingInvitationsQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public GetPendingInvitationsQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<PendingInvitationsListResponse>> Handle(
            GetPendingInvitationsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty || request.UserId != _userIdentifierProvider.UserId)
            {
                return Maybe<PendingInvitationsListResponse>.None;
            }

            PendingInvitationsListResponse.PendingInvitationModel[] invitations = await (
                from invitation in _dbContext.Set<Invitation>().AsNoTracking()
                join user in _dbContext.Set<User>().AsNoTracking()
                    on invitation.UserId equals user.Id
                join groupEvent in _dbContext.Set<GroupEvent>().AsNoTracking()
                    on invitation.EventId equals groupEvent.Id
                join friend in _dbContext.Set<User>().AsNoTracking()
                    on groupEvent.UserId equals friend.Id
                where invitation.UserId == request.UserId &&
                      invitation.CompletedOnUtc == null
                select new PendingInvitationsListResponse.PendingInvitationModel
                {
                    Id = invitation.Id,
                    FriendId = friend.Id,
                    FriendName = friend.FirstName.Value + " " + friend.LastName.Value,
                    EventName = groupEvent.Name.Value,
                    EventDateTimeUtc = groupEvent.DateTimeUtc,
                    CreatedOnUtc = invitation.CreatedOnUtc
                }).ToArrayAsync(cancellationToken);

            var response = new PendingInvitationsListResponse(invitations);

            return response;
        }
    }
}
