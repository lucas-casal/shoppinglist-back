using AutoMapper;
using Shoppinglist_back.Dtos.ShoppingListDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class ShoppingListProfile : Profile
{
    public ShoppingListProfile()
    {
        CreateMap<CreateShoppingListDto, ShoppingList>();
        CreateMap<ShoppingList, ReadShoppingListDto>().ForMember(dto => dto.ReadRelationMLDtos, opt => opt.MapFrom(shoppingList => shoppingList.RelationMembersLists));
        CreateMap<ReadShoppingListDto, ShoppingList>();
    }
}
