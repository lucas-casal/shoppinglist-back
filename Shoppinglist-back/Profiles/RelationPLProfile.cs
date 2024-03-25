using AutoMapper;
using Shoppinglist_back.Dtos.RelationProductsListsDto;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class RelationPLProfile : Profile
{
    public RelationPLProfile()
    {
        CreateMap<CreateRelationProductsListsDto, RelationProductsLists>();
        CreateMap<RelationProductsLists, ReadRelationProductsListsDto>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                                                                        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ShoppingList.Title));
    }
}
