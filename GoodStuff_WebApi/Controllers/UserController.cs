using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DataAccess.DBSets;
using Model.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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

        return BadRequest(new { message = "User already exists" });
    }

    [HttpGet]
    [Authorize(Roles = "Base")]
    [Route("getuserbyemail/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await userService.GetUserByEmail(email);
        if (user == null)
            return NotFound(new { message = "User not found" });
        return Ok(user);
    }
}