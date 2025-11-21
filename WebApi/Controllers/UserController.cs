using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Features.User.Commands.SignUp;
using Model.Features.User.Queries.SignIn;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
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

        logger.LogInformation("Successfully signed in user {Email}. Called {SignUpName} by {Unknown}", signInQuery.Email, nameof(SignUp), User.FindFirst("appid")?.Value ?? "Unknown");
        return Ok(user);
    }
}