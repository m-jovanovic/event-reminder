using System;
using System.Threading.Tasks;
using EventReminder.Application.Attendees.Queries.GetAttendeesForEventId;
using EventReminder.Contracts.Attendees;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Services.Api.Contracts;
using EventReminder.Services.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReminder.Services.Api.Controllers
{
    public sealed class AttendeesController : ApiController
    {
        public AttendeesController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet(ApiRoutes.Attendees.Get)]
        [ProducesResponseType(typeof(AttendeeListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid groupEventId) =>
            await Maybe<GetAttendeesForGroupEventIdQuery>
                .From(new GetAttendeesForGroupEventIdQuery(groupEventId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);
    }
}
