using AutoMapper;
using Shoppinglist_back.Dtos.ShoppingListDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class ShoppingListProfile : Profile
{
    public ShoppingListProfile()
    {
        CreateMap<CreateShoppingListDto, ShoppingList>();
        CreateMap<ShoppingList, ReadShoppingListDto>().ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.RelationMembersLists));
        CreateMap<ReadShoppingListDto, ShoppingList>();
    }
}
