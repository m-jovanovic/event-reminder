using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Contracts.Common;
using EventReminder.Contracts.PersonalEvents;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.PersonalEvents.Queries.GetPersonalEvents
{
    /// <summary>
    /// Represents the query for getting the paged list of the users personal events.
    /// </summary>
    public sealed class GetPersonalEventsQuery : IQuery<Maybe<PagedList<PersonalEventResponse>>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonalEventsQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="name">The name search.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">The page size.</param>
        public GetPersonalEventsQuery(
            Guid userId,
            string name,
            int? categoryId,
            DateTime? startDate,
            DateTime? endDate,
            int page,
            int pageSize)
        {
            UserId = userId;
            Name = name;
            CategoryId = categoryId;
            StartDate = startDate;
            EndDate = endDate;
            Page = page;
            PageSize = Math.Min(Math.Max(pageSize, 0), 100);
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets or sets the name search.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public int? CategoryId { get; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate { get; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? EndDate { get; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Gets or sets the page size. The max page size is 100.
        /// </summary>
        public int PageSize { get; }
    }
}
