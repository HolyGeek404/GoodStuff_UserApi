using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Features.User.Commands.SignUp;
using Model.Features.User.Queries;
using Model.Features.User.Queries.SignIn;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : Controller
{
    [HttpPost]
    [Authorize(Roles = "SignUp")]
    [Route("signup")]
    public async Task<IActionResult> SignUp(SignUpCommand signUpCommand)
    {
        var result = await mediator.Send(signUpCommand);
        if (result)
        {
            return CreatedAtAction(nameof(SignIn), new { email = signUpCommand.Email }, signUpCommand);
        }

        return BadRequest();
    }

    [HttpGet]
    [Authorize(Roles = "SignIn")]
    [Route("signin")]
    public async Task<IActionResult> SignIn(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return BadRequest();

        var user = await mediator.Send(new SignInQuery { Email = email, Password = password });

        if (user == null)
            return Unauthorized();

        return Ok(user);
    }
}