using System;
using System.Threading.Tasks;
using EventReminder.Application.FriendshipRequests.Commands.AcceptFriendshipRequest;
using EventReminder.Application.FriendshipRequests.Commands.RejectFriendshipRequest;
using EventReminder.Application.FriendshipRequests.Queries.GetFriendshipRequestById;
using EventReminder.Application.FriendshipRequests.Queries.GetPendingFriendshipRequests;
using EventReminder.Application.FriendshipRequests.Queries.GetSentFriendshipRequests;
using EventReminder.Contracts.FriendshipRequests;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Services.Api.Contracts;
using EventReminder.Services.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReminder.Services.Api.Controllers
{
    public sealed class FriendshipRequestsController : ApiController
    {
        public FriendshipRequestsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet(ApiRoutes.FriendshipRequests.GetById)]
        [ProducesResponseType(typeof(FriendshipRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid friendshipRequestId) =>
            await Maybe<GetFriendshipRequestByIdQuery>
                .From(new GetFriendshipRequestByIdQuery(friendshipRequestId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.FriendshipRequests.GetPending)]
        [ProducesResponseType(typeof(PendingFriendshipRequestsListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPending(Guid userId) =>
            await Maybe<GetPendingFriendshipRequestsQuery>
                .From(new GetPendingFriendshipRequestsQuery(userId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.FriendshipRequests.GetSent)]
        [ProducesResponseType(typeof(SentFriendshipRequestsListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSent(Guid userId) =>
            await Maybe<GetSentFriendshipRequestsQuery>
                .From(new GetSentFriendshipRequestsQuery(userId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpPost(ApiRoutes.FriendshipRequests.Accept)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Accept(Guid friendshipRequestId) =>
            await Result.Success(new AcceptFriendshipRequestCommand(friendshipRequestId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRoutes.FriendshipRequests.Reject)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reject(Guid friendshipRequestId) =>
            await Result.Success(new RejectFriendshipRequestCommand(friendshipRequestId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
