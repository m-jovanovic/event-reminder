using System;
using System.Threading.Tasks;
using EventReminder.Application.PersonalEvents.Commands.CancelPersonalEvent;
using EventReminder.Application.PersonalEvents.Commands.CreatePersonalEvent;
using EventReminder.Application.PersonalEvents.Commands.UpdatePersonalEvent;
using EventReminder.Application.PersonalEvents.Queries.GetPersonalEventById;
using EventReminder.Application.PersonalEvents.Queries.GetPersonalEvents;
using EventReminder.Contracts.Common;
using EventReminder.Contracts.PersonalEvents;
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
    public sealed class PersonalEventsController : ApiController
    {
        public PersonalEventsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet(ApiRoutes.PersonalEvents.GetById)]
        [ProducesResponseType(typeof(DetailedPersonalEventResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid personalEventId) =>
            await Maybe<GetPersonalEventByIdQuery>
                .From(new GetPersonalEventByIdQuery(personalEventId))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpGet(ApiRoutes.PersonalEvents.Get)]
        [ProducesResponseType(typeof(PagedList<PersonalEventResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(
            Guid userId,
            string name,
            int? categoryId,
            DateTime? startDate,
            DateTime? endDate,
            int page,
            int pageSize) =>
            await Maybe<GetPersonalEventsQuery>
                .From(new GetPersonalEventsQuery(userId, name, categoryId, startDate, endDate, page, pageSize))
                .Bind(query => Mediator.Send(query))
                .Match(Ok, NotFound);

        [HttpPost(ApiRoutes.PersonalEvents.Create)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreatePersonalEventRequest createPersonalEventRequest) =>
            await Result.Create(createPersonalEventRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreatePersonalEventCommand(request.UserId, request.Name, request.CategoryId, request.DateTimeUtc))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpPut(ApiRoutes.PersonalEvents.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid personalEventId, UpdatePersonalEventRequest updatePersonalEventRequest) =>
            await Result.Create(updatePersonalEventRequest, DomainErrors.General.UnProcessableRequest)
                .Ensure(request => request.PersonalEventId == personalEventId, DomainErrors.General.UnProcessableRequest)
                .Map(request => new UpdatePersonalEventCommand(request.PersonalEventId, request.Name, request.DateTimeUtc))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [HttpDelete(ApiRoutes.PersonalEvents.Cancel)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cancel(Guid personalEventId) =>
            await Result.Success(new CancelPersonalEventCommand(personalEventId))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
