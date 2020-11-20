using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Application.FriendshipRequests.Queries.GetFriendshipRequestById;
using EventReminder.Application.FriendshipRequests.Queries.GetPendingFriendshipRequests;
using EventReminder.Contracts.FriendshipRequests;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.FriendshipRequests.Queries.GetSentFriendshipRequests
{
    /// <summary>
    /// Represents the <see cref="GetPendingFriendshipRequestsQuery"/> handler.
    /// </summary>
    internal sealed class GetSentFriendshipRequestsQueryHandler
        : IQueryHandler<GetSentFriendshipRequestsQuery, Maybe<SentFriendshipRequestsListResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFriendshipRequestByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public GetSentFriendshipRequestsQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<SentFriendshipRequestsListResponse>> Handle(
            GetSentFriendshipRequestsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty || request.UserId != _userIdentifierProvider.UserId)
            {
                return Maybe<SentFriendshipRequestsListResponse>.None;
            }

            SentFriendshipRequestsListResponse.SentFriendshipRequestModel[] friendshipRequests = await (
                    from friendshipRequest in _dbContext.Set<FriendshipRequest>().AsNoTracking()
                    join user in _dbContext.Set<User>().AsNoTracking()
                        on friendshipRequest.FriendId equals user.Id
                    where friendshipRequest.UserId == request.UserId && friendshipRequest.CompletedOnUtc == null
                    select new SentFriendshipRequestsListResponse.SentFriendshipRequestModel
                    {
                        Id = friendshipRequest.Id,
                        FriendId = user.Id,
                        FriendName = user.FirstName.Value + " " + user.LastName.Value,
                        CreatedOnUtc = friendshipRequest.CreatedOnUtc
                    }).ToArrayAsync(cancellationToken);

            var response = new SentFriendshipRequestsListResponse(friendshipRequests);

            return response;
        }
    }
}
