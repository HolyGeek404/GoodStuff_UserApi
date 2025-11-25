using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Features.User.Commands.SignUp;
using Model.Features.User.Queries.SignIn;
using Model.Models.User;
using Model.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator, ILogger<UserController> logger, IUserSessionService sessionService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "SignUp")]
    [Route("signup")]
    public async Task<IActionResult> SignUp(SignUpCommand signUpCommand)
    {
        logger.LogInformation("Called {SignUpName} by {Unknown}", nameof(SignUp), User.FindFirst("appid")?.Value ?? "Unknown");

        var result = await mediator.Send(signUpCommand);
        if (result)
        {
            logger.LogInformation("Successfully registered new user {Email}. Called by {Unknown}", signUpCommand.Email, User.FindFirst("appid")?.Value ?? "Unknown");
            return CreatedAtAction(nameof(SignIn), new { email = signUpCommand.Email }, signUpCommand);
        }

        logger.LogInformation("Couldn't register user {Email}. Called by {Unknown}", signUpCommand.Email, User.FindFirst("appid")?.Value ?? "Unknown");
        return BadRequest();
    }

    [HttpPost]
    [Authorize(Roles = "SignIn")]
    [Route("signin")]
    public async Task<IActionResult> SignIn([FromBody]  SignInQuery signInQuery)
    {
        logger.LogInformation("Called {SignUpName} by {Unknown}", nameof(SignUp), User.FindFirst("appid")?.Value ?? "Unknown");

        if (string.IsNullOrEmpty(signInQuery.Email) || string.IsNullOrEmpty(signInQuery.Password))
        {
            logger.LogInformation($"Couldn't sign in because email or password is empty");
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

        logger.LogInformation("Successfully signed in user {Email}. Called {SignUpName} by {Unknown}", signInQuery.Email, nameof(SignUp), User.FindFirst("appid")?.Value ?? "Unknown");
        return Ok(userModel);
    }

    [HttpPost]
    [Route("signout")]
    [Authorize(Roles = "SignOut")]
    public IActionResult SignOut([FromBody] SignOutRequest signOutRequest)
    {
        logger.LogInformation("SignOut request received. SessionId: {SessionId}", signOutRequest);

        try
        {
            logger.LogInformation("Clearing cached user data for SessionId: {SessionId}", signOutRequest);

            sessionService.ClearUserCachedData(signOutRequest.SessionId);

            logger.LogInformation("Successfully signed out user. SessionId: {SessionId}", signOutRequest);

            return Ok(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while signing out user. SessionId: {SessionId}", signOutRequest.SessionId);
            return StatusCode(500, "Internal server error during sign-out.");
        }
    }
}