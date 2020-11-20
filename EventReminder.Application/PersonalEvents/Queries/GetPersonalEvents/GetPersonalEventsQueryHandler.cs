using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Data;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.Common;
using EventReminder.Contracts.PersonalEvents;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace EventReminder.Application.PersonalEvents.Queries.GetPersonalEvents
{
    /// <summary>
    /// Represents the <see cref="GetPersonalEventsQuery"/> handler.
    /// </summary>
    internal sealed class GetPersonalEventsQueryHandler : IQueryHandler<GetPersonalEventsQuery, Maybe<PagedList<PersonalEventResponse>>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserIdentifierProvider _userIdentifierProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonalEventsQueryHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        public GetPersonalEventsQueryHandler(IDbContext dbContext, IUserIdentifierProvider userIdentifierProvider)
        {
            _dbContext = dbContext;
            _userIdentifierProvider = userIdentifierProvider;
        }

        /// <inheritdoc />
        public async Task<Maybe<PagedList<PersonalEventResponse>>> Handle(
            GetPersonalEventsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty || request.UserId != _userIdentifierProvider.UserId)
            {
                return Maybe<PagedList<PersonalEventResponse>>.None;
            }

            bool shouldSearchCategory = request.CategoryId != null && Category.ContainsValue(request.CategoryId.Value);

            IQueryable<PersonalEventResponse> personalEventResponses =
                from personalEvent in _dbContext.Set<PersonalEvent>().AsNoTracking()
                join user in _dbContext.Set<User>().AsNoTracking()
                    on personalEvent.UserId equals user.Id
                where user.Id == request.UserId &&
                      personalEvent.UserId == request.UserId &&
                      !personalEvent.Cancelled &&
                      (!shouldSearchCategory || personalEvent.Category.Value == request.CategoryId) &&
                      (request.Name == null || request.Name == "" || personalEvent.Name.Value.Contains(request.Name)) &&
                      (request.StartDate == null || personalEvent.DateTimeUtc >= request.StartDate) &&
                      (request.EndDate == null || personalEvent.DateTimeUtc <= request.EndDate)
                orderby personalEvent.DateTimeUtc descending
                select new PersonalEventResponse
                {
                    Id = personalEvent.Id,
                    Name = personalEvent.Name.Value,
                    CategoryId = personalEvent.Category.Value,
                    DateTimeUtc = personalEvent.DateTimeUtc,
                    CreatedOnUtc = personalEvent.CreatedOnUtc
                };

            int totalCount = await personalEventResponses.CountAsync(cancellationToken);

            PersonalEventResponse[] personalEventResponsesPage = await personalEventResponses
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArrayAsync(cancellationToken);

            foreach (PersonalEventResponse personalEventResponse in personalEventResponsesPage)
            {
                personalEventResponse.Category = Category.FromValue(personalEventResponse.CategoryId).Value.Name;
            }

            return new PagedList<PersonalEventResponse>(personalEventResponsesPage, request.Page, request.PageSize, totalCount);
        }
    }
}
