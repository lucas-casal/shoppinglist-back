using AutoMapper;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class RelationMLProfile : Profile
{
    public RelationMLProfile()
    {
        CreateMap<CreateRelationMembersListsDto, RelationMembersLists>();
        CreateMap<RelationMembersLists, ReadRelationMembersListsThroughListDto>();
    }
}
