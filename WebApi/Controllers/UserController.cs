using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DataAccess.DBSets;
using Model.Features.User.Commands;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : Controller
{
    [HttpPost]
    // [Authorize(Roles = "Base")]
    [Route("signup")]
    public async Task<IActionResult> SignUp(SignUpCommand signUpCommand)
    {
        var result = await mediator.Send(signUpCommand);
        if (result)
        {
            return CreatedAtAction(nameof(GetUserByEmail), new { email = signUpCommand.Email }, signUpCommand);
        }

        return BadRequest();
    }

    // [HttpGet]
    // [Authorize(Roles = "Base")]
    // [Route("signin")]
    // public async Task<IActionResult> SignIn(string email, string password)
    // {
    //     Console.WriteLine($"Email: {email}, Password: {password}");
    //     if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    //         return BadRequest();

    //     var user = await userService.SignIn(email, password);

    //     if (user == null)
    //         return Unauthorized();

    //     return Ok(user);
    // }

    [HttpGet]
    [Authorize(Roles = "Base")]
    [Route("getuserbyemail")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        //var user = await userService.GetUserByEmail(email);
        var user = new User();
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}