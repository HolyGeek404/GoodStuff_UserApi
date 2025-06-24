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
        logger.LogInformation($"Called {nameof(SignUp)} by {User.FindFirst("appid")?.Value ?? "Unknown"}");

        var result = await mediator.Send(signUpCommand);
        if (result)
        {
            logger.LogInformation($"Successfully registered new user {signUpCommand.Email}. Called by {User.FindFirst("appid")?.Value ?? "Unknown"}");
            return CreatedAtAction(nameof(SignIn), new { email = signUpCommand.Email }, signUpCommand);
        }

        logger.LogInformation($"Couldn't register user {signUpCommand.Email}. Called by {User.FindFirst("appid")?.Value ?? "Unknown"}");
        return BadRequest();
    }

    [HttpGet]
    [Authorize(Roles = "SignIn")]
    [Route("signin")]
    public async Task<IActionResult> SignIn(string email, string password)
    {
        logger.LogInformation($"Called {nameof(SignUp)} by {User.FindFirst("appid")?.Value ?? "Unknown"}");

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            logger.LogInformation($"Couldn't sign in because email or password is empty");
            return BadRequest();
        }

        var user = await mediator.Send(new SignInQuery { Email = email, Password = password });

        if (user == null)
        {
            return Unauthorized();
        }

        logger.LogInformation($"Successfully signed in user {email}. Called {nameof(SignUp)} by {User.FindFirst("appid")?.Value ?? "Unknown"}");
        return Ok(user);
    }
}