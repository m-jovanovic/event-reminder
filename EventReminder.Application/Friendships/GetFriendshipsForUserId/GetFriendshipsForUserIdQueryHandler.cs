using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Authentication;
using EventReminder.Application.Abstractions.Data;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.Common;
using EventReminder.Contracts.Friendships;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Friendships;
using EventReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.Friendships.GetFriendshipsForUserId
{
    /// <summary>
    /// Represents the <see cref="GetFriendshipsForUserIdQuery"/> handler.
    /// </summary>
    internal sealed class GetFriendshipsForUserIdQueryHandler : IQueryHandler<GetFriendshipsForUserIdQuery, Maybe<PagedList<FriendshipResponse>>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFriendshipsForUserIdQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public GetFriendshipsForUserIdQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<PagedList<FriendshipResponse>>> Handle(GetFriendshipsForUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userIdentifierProvider.UserId)
            {
                return Maybe<PagedList<FriendshipResponse>>.None;
            }

            IQueryable<FriendshipResponse> friendshipResponsesQuery =
                from friendship in _dbContext.Set<Friendship>().AsNoTracking()
                join user in _dbContext.Set<User>().AsNoTracking()
                    on friendship.UserId equals user.Id
                join friend in _dbContext.Set<User>().AsNoTracking()
                    on friendship.FriendId equals friend.Id
                where friendship.UserId == request.UserId
                orderby friend.FirstName.Value
                select new FriendshipResponse
                {
                    UserId = user.Id,
                    UserEmail = user.Email.Value,
                    UserName = user.FirstName.Value + " " + user.LastName.Value,
                    FriendId = friend.Id,
                    FriendEmail = friend.Email.Value,
                    FriendName = friend.FirstName.Value + " " + friend.LastName.Value,
                    CreatedOnUtc = friendship.CreatedOnUtc
                };

            int totalCount = await friendshipResponsesQuery.CountAsync(cancellationToken);

            IEnumerable<FriendshipResponse> friendshipResponsesPage = await friendshipResponsesQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            return new PagedList<FriendshipResponse>(friendshipResponsesPage, request.Page, request.PageSize, totalCount);
        }
    }
}
