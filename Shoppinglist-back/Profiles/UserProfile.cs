using AutoMapper;
using Shoppinglist_back.Dtos.UserDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class UserProfile : Profile
{
    public UserProfile() 
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<LoginUserDto, User>();
    }
}
