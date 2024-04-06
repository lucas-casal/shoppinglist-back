using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shoppinglist_back.Controllers;


[ApiController]
[Route("[Controller]")]
public class AccessController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "HasToken")]
    public IActionResult Get([FromHeader(Name = "Authorization")] string token)
    {
        return Ok(token);
    }
}
