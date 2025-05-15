using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    public IActionResult SignUp()
    {
        return Empty;
    }
}