using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Services.Api.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventReminder.Services.Api.Infrastructure
{
    [Authorize]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        protected ApiController(IMediator mediator) => Mediator = mediator;

        protected IMediator Mediator { get; }

        /// <summary>
        /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
        /// response based on the specified <see cref="Result"/>.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
        protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));

        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> that produces a <see cref="StatusCodes.Status200OK"/>.
        /// </summary>
        /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
        /// <returns></returns>
        protected new IActionResult Ok(object value) => base.Ok(value);

        /// <summary>
        /// Creates an <see cref="NotFoundResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
        /// </summary>
        /// <returns>The created <see cref="NotFoundResult"/> for the response.</returns>
        protected new IActionResult NotFound() => base.NotFound();
    }
}
