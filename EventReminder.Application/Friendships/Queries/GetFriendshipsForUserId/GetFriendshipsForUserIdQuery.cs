using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.Common;
using EventReminder.Contracts.Friendships;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.Friendships.Queries.GetFriendshipsForUserId
{
    /// <summary>
    /// Represents the query for getting the friendships for the user identifier.
    /// </summary>
    public sealed class GetFriendshipsForUserIdQuery : IQuery<Maybe<PagedList<FriendshipResponse>>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFriendshipsForUserIdQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="page">The current page.</param>
        /// <param name="pageSize">The page size.</param>
        public GetFriendshipsForUserIdQuery(Guid userId, int page, int pageSize)
        {
            UserId = userId;
            Page = page;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
        
        /// <summary>
        /// Gets the current page.
        /// </summary>
        public int Page { get; }
        
        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize { get; }
    }
}
