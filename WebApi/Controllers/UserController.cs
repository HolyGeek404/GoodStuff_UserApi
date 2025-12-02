using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Features.User.Commands.AccountVerification;
using Model.Features.User.Commands.SignOutCommand;
using Model.Features.User.Commands.SignUp;
using Model.Features.User.Queries.SignIn;
using Model.Models.User;
using Model.Services;
using Model.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator, ILogger<UserController> logger, IUserSessionService sessionService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "SignUp")]
    [Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand signUpCommand)
    {
        Logs.LogCalledSignupnameByUnknown(logger, nameof(SignUp), User.FindFirst("appid")?.Value ?? "Unknown");

        var result = await mediator.Send(signUpCommand);
        if (result)
        {
            Logs.LogSuccessfullyRegisteredNewUserEmailCalledByUnknown(logger, signUpCommand.Email, User.FindFirst("appid")?.Value ?? "Unknown");
            var userModel = new UserModel
            {
                Email = signUpCommand.Email,
                Name = signUpCommand.Name,
                Surname = signUpCommand.Surname
            };
            return CreatedAtAction(nameof(SignIn), new { email = signUpCommand.Email }, userModel);
        }

        Logs.LogCouldnTRegisterUserEmailCalledByUnknown(logger, signUpCommand.Email, User.FindFirst("appid")?.Value ?? "Unknown");
        return BadRequest();
    }

    [HttpPost]
    [Authorize(Roles = "SignIn")]
    [Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInQuery signInQuery)
    {
        Logs.LogCalledSignupnameByUnknown(logger, nameof(SignUp), User.FindFirst("appid")?.Value ?? "Unknown");

        if (string.IsNullOrEmpty(signInQuery.Email) || string.IsNullOrEmpty(signInQuery.Password))
        {
            Logs.LogCouldnTSignInBecauseEmailOrPasswordIsEmpty(logger);
            return BadRequest();
        }

        var user = await mediator.Send(signInQuery);
        
        if (user == null)
        {
            return Unauthorized();
        }
        
        var sessionId = sessionService.CreateSession(user);
        var userModel = new UserModel
        {
            Email = user.Email,
            Name = user.Name,
            Surname = user.Surname,
            SessionId = sessionId,
        };

        Logs.LogSuccessfullySignedInUserEmailCalledSignupnameByUnknown(logger, signInQuery.Email, nameof(SignUp), User.FindFirst("appid")?.Value ?? "Unknown");
        return Ok(userModel);
    }

    [HttpPost]
    [Route("signout")]
    [Authorize(Roles = "SignOut")]
    public async Task<IActionResult> SignOut([FromBody] SignOutCommand signOutCommand)
    {
        Logs.LogSignoutRequestReceivedSessionidSessionid(logger, signOutCommand);

        try
        {
            Logs.LogClearingCachedUserDataForSessionidSessionid(logger, signOutCommand);
        
            var result = await mediator.Send(signOutCommand);

            Logs.LogSuccessfullySignedOutUserSessionidSessionid(logger, signOutCommand);

            return Ok(true);
        }
        catch (Exception ex)
        {
            Logs.LogAnErrorOccurredWhileSigningOutUserSessionidSessionid(logger, ex, signOutCommand.SessionId);
            return StatusCode(500, "Internal server error during sign-out.");
        }
    }

    [HttpPost]
    [Route("accountverification")]
    public async Task<IActionResult> AccountVerification([FromBody] AccountVerificationCommand accountVerificationCommand)
    {
        Logs.LogAccountverificationEndpointCalledWithPayloadPayload(logger, accountVerificationCommand);

        try
        {
            var result = await mediator.Send(accountVerificationCommand);

            Logs.LogAccountVerificationSuccessfulForAccountAccount(logger, accountVerificationCommand.Email);

            return Ok(result);
        }
        catch (Exception ex)
        {
            Logs.LogErrorOccurredDuringVerificationForAccountAccount(logger, ex, accountVerificationCommand.Email);
            return StatusCode(500, "An error occurred while verifying the account.");
        }
    }
    
    
}