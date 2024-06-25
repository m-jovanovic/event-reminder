using EventReminder.Application.Authentication.Login;
using EventReminder.Application.Users.CreateUser;
using EventReminder.Contracts.Authentication;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Services.Api.Contracts;
using EventReminder.Services.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventReminder.Services.Api.Controllers
{
    [AllowAnonymous]
    public sealed class AuthenticationController : ApiController
    {
        public AuthenticationController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequest loginRequest) =>
            await Result.Create(loginRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new LoginCommand(request.Email, request.Password))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authentication.Register)]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(RegisterRequest registerRequest) =>
            await Result.Create(registerRequest, DomainErrors.General.UnProcessableRequest)
                .Map(request => new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.Password))
                .Bind(command => Mediator.Send(command))
                .Match(Ok, BadRequest);
    }
}
