using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shoppinglist_back.Dtos.UserDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    public async Task SignUp(CreateUserDto userDto)
    {
        userDto.Username = userDto.Username.Replace(" ", "_");
        User user = _mapper.Map<User>(userDto);

        IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded) throw new ApplicationException("Confira se a senha tem letras maiúsculas, minúsculas, número e caractere especial!");
    }

    public async Task<string> Login(LoginUserDto userDto)
    {
        var result = await _signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, true, false);
       
        if (!result.Succeeded) throw new ApplicationException("Não foi possível realizar o login!");

        var user = _signInManager.UserManager.Users.FirstOrDefault(e => e.NormalizedUserName == userDto.UserName.ToUpper());

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}
