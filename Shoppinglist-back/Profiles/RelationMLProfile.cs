using AutoMapper;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class RelationMLProfile : Profile
{
    public RelationMLProfile()
    {
        CreateMap<CreateRelationMembersListsDto, RelationMembersLists>();
        CreateMap<RelationMembersLists, ReadRelationMembersListsThroughListDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name));
        CreateMap<RelationMembersLists, ReadRelationMembersListsCompleteDto>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                                                                           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ShoppingList.Title));
    }
}
