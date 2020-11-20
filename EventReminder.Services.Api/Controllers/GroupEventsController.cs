using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventReminder.Application.GroupEvents.Commands.CancelGroupEvent;
using EventReminder.Application.GroupEvents.Commands.CreateGroupEvent;
using EventReminder.Application.GroupEvents.Commands.InviteFriendToGroupEvent;
using EventReminder.Application.GroupEvents.Commands.UpdateGroupEvent;
using EventReminder.Application.GroupEvents.Queries.Get10MostRecentAttendingGroupEvents;
using EventReminder.Application.GroupEvents.Queries.GetGroupEventById;
using EventReminder.Application.GroupEvents.Queries.GetGroupEvents;
using EventReminder.Contracts.Common;
using EventReminder.Contracts.GroupEvents;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Services.Api.Contracts;
using EventReminder.Services.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReminder.Services.Api.Controllers
{
    public sealed class GroupEventsController : ApiController
    {
        public GroupEventsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet(ApiRoutes.GroupEvents.GetById)]
        [ProducesResponseType(typeof(DetailedGroupEventResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid groupEventId) =>
            await Maybe<GetGroupEventByIdQuery>
                .From(new GetGroupEventByIdQuery(groupEventId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.GroupEvents.Get)]
        [ProducesResponseType(typeof(PagedList<GroupEventResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(
            Guid userId,
            string name,
            int? categoryId,
            DateTime? startDate,
            DateTime? endDate,
            int page,
            int pageSize) =>
            await Maybe<GetGroupEventsQuery>
                .From(new GetGroupEventsQuery(userId, name, categoryId, startDate, endDate, page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.GroupEvents.GetMostRecentAttending)]
        [ProducesResponseType(typeof(IReadOnlyCollection<GroupEventResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMostRecentAttending(Guid userId) =>
            await Maybe<Get10MostRecentAttendingGroupEventsQuery>
                .From(new Get10MostRecentAttendingGroupEventsQuery(userId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpPost(ApiRoutes.GroupEvents.Create)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateGroupEventRequest createGroupEventRequest) =>
            await Result.Create(createGroupEventRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateGroupEventCommand(request.UserId, request.Name, request.CategoryId, request.DateTimeUtc))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPut(ApiRoutes.GroupEvents.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid groupEventId, UpdateGroupEventRequest updateGroupEventRequest) =>
            await Result.Create(updateGroupEventRequest, DomainErrors.General.UnProcessableRequest)
                .Ensure(request => request.GroupEventId == groupEventId, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdateGroupEventCommand(request.GroupEventId, request.Name, request.DateTimeUtc))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRoutes.GroupEvents.InviteFriend)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InviteFriend(Guid groupEventId, InviteFriendToGroupEventRequest inviteFriendToGroupEventRequest) =>
            await Result.Create(inviteFriendToGroupEventRequest, DomainErrors.General.UnProcessableRequest)
                .Ensure(request => request.GroupEventId == groupEventId, DomainErrors.General.UnProcessableRequest)
                .Map(request => new InviteFriendToGroupEventCommand(request.GroupEventId, request.FriendId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpDelete(ApiRoutes.GroupEvents.Cancel)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cancel(Guid groupEventId) =>
            await Result.Success(new CancelGroupEventCommand(groupEventId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
