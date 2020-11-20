using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.Friendships;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.Friendships.Queries.GetFriendship
{
    /// <summary>
    /// Represents the <see cref="GetFriendshipQuery"/> handler.
    /// </summary>
    internal sealed class GetFriendshipQueryHandler : IQueryHandler<GetFriendshipQuery, Maybe<FriendshipResponse>>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFriendshipQueryHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="dbContext">The database context.</param>
        public GetFriendshipQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Maybe<FriendshipResponse>> Handle(
            GetFriendshipQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty || request.FriendId == Guid.Empty || request.UserId != _userIdentifierProvider.UserId)
            {
                return Maybe<FriendshipResponse>.None;
            }

            FriendshipResponse response = await (
                from friendship in _dbContext.Set<Friendship>().AsNoTracking()
                join user in _dbContext.Set<User>().AsNoTracking()
                    on friendship.UserId equals user.Id
                join friend in _dbContext.Set<User>().AsNoTracking()
                    on friendship.FriendId equals friend.Id
                where friendship.UserId == request.UserId && friendship.FriendId == request.FriendId
                select new FriendshipResponse
                {
                    UserId = user.Id,
                    UserEmail = user.Email.Value,
                    UserName = user.FirstName.Value + " " + user.LastName.Value,
                    FriendId = friend.Id,
                    FriendEmail = friend.Email.Value,
                    FriendName = friend.FirstName.Value + " " + friend.LastName.Value,
                    CreatedOnUtc = friendship.CreatedOnUtc
                }).FirstOrDefaultAsync(cancellationToken);

            if (response is null)
            {
                return Maybe<FriendshipResponse>.None;
            }

            if (response.UserId != _userIdentifierProvider.UserId || response.FriendId != _userIdentifierProvider.UserId)
            {
                return Maybe<FriendshipResponse>.None;
            }

            return response;
        }
    }
}
