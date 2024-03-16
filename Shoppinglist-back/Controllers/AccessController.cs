using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shoppinglist_back.Controllers;


[ApiController]
[Route("[Controller]")]
public class AccessController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "HasToken")]
    public IActionResult Get()
    {
        return Ok("Access granted!!");
    }
}
