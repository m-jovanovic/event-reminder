using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.FriendshipRequests;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.FriendshipRequests.Queries.GetFriendshipRequestById
{
    /// <summary>
    /// Represents the <see cref="GetFriendshipRequestByIdQuery"/> handler.
    /// </summary>
    internal sealed class GetFriendshipRequestByIdQueryHandler
        : IQueryHandler<GetFriendshipRequestByIdQuery, Maybe<FriendshipRequestResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFriendshipRequestByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public GetFriendshipRequestByIdQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<FriendshipRequestResponse>> Handle(
            GetFriendshipRequestByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request.FriendshipRequestId == Guid.Empty)
            {
                return Maybe<FriendshipRequestResponse>.None;
            }

            FriendshipRequestResponse response = await (
                from friendshipRequest in _dbContext.Set<FriendshipRequest>().AsNoTracking()
                join user in _dbContext.Set<User>().AsNoTracking()
                    on friendshipRequest.UserId equals user.Id
                join friend in _dbContext.Set<User>().AsNoTracking()
                    on friendshipRequest.FriendId equals friend.Id
                where friendshipRequest.Id == request.FriendshipRequestId && friendshipRequest.CompletedOnUtc == null
                select new FriendshipRequestResponse
                {
                    UserId = user.Id,
                    UserEmail = user.Email.Value,
                    UserName = user.FirstName.Value + " " + user.LastName.Value,
                    FriendId = friend.Id,
                    FriendEmail = friend.Email.Value,
                    FriendName = friend.FirstName.Value + " " + friend.LastName.Value,
                    CreatedOnUtc = friendshipRequest.CreatedOnUtc
                }).FirstOrDefaultAsync(cancellationToken);

            if (response is null)
            {
                return Maybe<FriendshipRequestResponse>.None;
            }

            if (response.UserId != _userIdentifierProvider.UserId || response.FriendId != _userIdentifierProvider.UserId)
            {
                return Maybe<FriendshipRequestResponse>.None;
            }

            return response;
        }
    }
}
