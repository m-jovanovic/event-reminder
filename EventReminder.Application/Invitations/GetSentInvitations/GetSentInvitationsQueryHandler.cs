using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Authentication;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Application.Invitations.GetPendingInvitations;
using EventReminder.Contracts.Invitations;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Events;
using EventReminder.Domain.Invitations;
using EventReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.Invitations.GetSentInvitations
{
    /// <summary>
    /// Represents the <see cref="GetPendingInvitationsQuery"/> handler.
    /// </summary>
    internal sealed class GetSentInvitationsQueryHandler
        : IQueryHandler<GetSentInvitationsQuery, Maybe<SentInvitationsListResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSentInvitationsQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public GetSentInvitationsQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<SentInvitationsListResponse>> Handle(
            GetSentInvitationsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty || request.UserId != _userIdentifierProvider.UserId)
            {
                return Maybe<SentInvitationsListResponse>.None;
            }

            SentInvitationsListResponse.SentInvitationModel[] invitations = await (
                from invitation in _dbContext.Set<Invitation>().AsNoTracking()
                join friend in _dbContext.Set<User>().AsNoTracking()
                    on invitation.UserId equals friend.Id
                join groupEvent in _dbContext.Set<GroupEvent>().AsNoTracking()
                    on invitation.EventId equals groupEvent.Id
                join user in _dbContext.Set<User>().AsNoTracking()
                    on groupEvent.UserId equals user.Id
                where user.Id == request.UserId &&
                      groupEvent.UserId == request.UserId &&
                      invitation.CompletedOnUtc == null
                select new SentInvitationsListResponse.SentInvitationModel
                {
                    Id = invitation.Id,
                    FriendId = friend.Id,
                    FriendName = friend.FirstName.Value + " " + friend.LastName.Value,
                    EventName = groupEvent.Name.Value,
                    EventDateTimeUtc = groupEvent.DateTimeUtc,
                    CreatedOnUtc = invitation.CreatedOnUtc
                }).ToArrayAsync(cancellationToken);

            var response = new SentInvitationsListResponse(invitations);

            return response;
        }
    }
}
