using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DataAccess.DBSets;
using Model.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : Controller
{
    [HttpPost]
    [Authorize(Roles = "Base")]
    [Route("signup")]
    public async Task<IActionResult> SignUp(User model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await userService.SignUp(model);
        if (result)
        {
            return CreatedAtAction(nameof(GetUserByEmail), new { email = model.Email }, model);
        }

        return BadRequest();
    }

    [HttpGet]
    [Authorize(Roles = "Base")]
    [Route("signin")]
    public async Task<IActionResult> SignIn(string email, string password)
    {
        Console.WriteLine($"Email: {email}, Password: {password}");
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return BadRequest();

        var user = await userService.SignIn(email, password);

        if (user == null)
            return Unauthorized();

        return Ok(user);
    }

    [HttpGet]   
    [Authorize(Roles = "Base")]
    [Route("getuserbyemail")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await userService.GetUserByEmail(email);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}