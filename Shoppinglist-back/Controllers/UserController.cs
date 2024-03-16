using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.UserDtos;
using Shoppinglist_back.Models;
using Shoppinglist_back.Services;

namespace Shoppinglist_back.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> SignUpUser(CreateUserDto userDto)
    {
        await _userService.SignUp(userDto);
        return Ok(userDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserDto userDto)
    {
        var token = await _userService.Login(userDto);
        return Ok(token);

    }


}
