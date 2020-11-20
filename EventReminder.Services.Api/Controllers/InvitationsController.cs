using System;
using System.Threading.Tasks;
using EventReminder.Application.Invitations.Commands.AcceptInvitation;
using EventReminder.Application.Invitations.Commands.RejectInvitation;
using EventReminder.Application.Invitations.Queries.GetInvitationById;
using EventReminder.Application.Invitations.Queries.GetPendingInvitations;
using EventReminder.Application.Invitations.Queries.GetSentInvitations;
using EventReminder.Contracts.Invitations;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Services.Api.Contracts;
using EventReminder.Services.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReminder.Services.Api.Controllers
{
    public sealed class InvitationsController : ApiController
    {
        public InvitationsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet(ApiRoutes.Invitations.GetById)]
        [ProducesResponseType(typeof(InvitationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid invitationId) =>
            await Maybe<GetInvitationByIdQuery>
                .From(new GetInvitationByIdQuery(invitationId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.Invitations.GetPending)]
        [ProducesResponseType(typeof(PendingInvitationsListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPending(Guid userId) =>
            await Maybe<GetPendingInvitationsQuery>
                .From(new GetPendingInvitationsQuery(userId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.Invitations.GetSent)]
        [ProducesResponseType(typeof(SentInvitationsListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSent(Guid userId) =>
            await Maybe<GetSentInvitationsQuery>
                .From(new GetSentInvitationsQuery(userId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpPost(ApiRoutes.Invitations.Accept)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Accept(Guid invitationId) =>
            await Result.Success(new AcceptInvitationCommand(invitationId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPost(ApiRoutes.Invitations.Reject)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reject(Guid invitationId) =>
            await Result.Success(new RejectInvitationCommand(invitationId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
