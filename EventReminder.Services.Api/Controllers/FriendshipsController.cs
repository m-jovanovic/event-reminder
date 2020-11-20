using System;
using System.Threading.Tasks;
using EventReminder.Application.Friendships.Commands.RemoveFriendship;
using EventReminder.Application.Friendships.Queries.GetFriendship;
using EventReminder.Application.Friendships.Queries.GetFriendshipsForUserId;
using EventReminder.Contracts.Common;
using EventReminder.Contracts.Friendships;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Services.Api.Contracts;
using EventReminder.Services.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReminder.Services.Api.Controllers
{
    public sealed class FriendshipsController : ApiController
    {
        public FriendshipsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet(ApiRoutes.Friendships.GetForUserId)]
        [ProducesResponseType(typeof(PagedList<FriendshipResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid userId, int page, int pageSize) =>
            await Maybe<GetFriendshipsForUserIdQuery>
                .From(new GetFriendshipsForUserIdQuery(userId, page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.Friendships.Get)]
        [ProducesResponseType(typeof(FriendshipResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid userId, Guid friendId) =>
            await Maybe<GetFriendshipQuery>
                .From(new GetFriendshipQuery(userId, friendId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpDelete(ApiRoutes.Friendships.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(Guid userId, Guid friendId) =>
            await Result.Success(new RemoveFriendshipCommand(userId, friendId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
