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

namespace EventReminder.Application.Invitations.Queries.GetInvitationById
{
    /// <summary>
    /// Represents the <see cref="GetInvitationByIdQuery"/> handler.
    /// </summary>
    internal sealed class GetInvitationByIdQueryHandler : IQueryHandler<GetInvitationByIdQuery, Maybe<InvitationResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInvitationByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public GetInvitationByIdQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<InvitationResponse>> Handle(GetInvitationByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.InvitationId == Guid.Empty)
            {
                return Maybe<InvitationResponse>.None;
            }

            InvitationResponse response = await (
                from invitation in _dbContext.Set<Invitation>().AsNoTracking()
                join user in _dbContext.Set<User>().AsNoTracking()
                    on invitation.UserId equals user.Id
                join groupEvent in _dbContext.Set<GroupEvent>().AsNoTracking()
                    on invitation.EventId equals groupEvent.Id
                join friend in _dbContext.Set<User>().AsNoTracking()
                    on groupEvent.UserId equals friend.Id
                where invitation.Id == request.InvitationId &&
                      invitation.UserId == _userIdentifierProvider.UserId &&
                      invitation.CompletedOnUtc == null
                select new InvitationResponse
                {
                    Id = invitation.Id,
                    EventName = groupEvent.Name.Value,
                    EventDateTimeUtc = groupEvent.DateTimeUtc,
                    FriendName = friend.FirstName.Value + " " + friend.LastName.Value,
                    CreatedOnUtc = invitation.CreatedOnUtc
                }).FirstOrDefaultAsync(cancellationToken);
            
            return response;
        }
    }
}
