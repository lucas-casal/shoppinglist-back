using AutoMapper;
using Shoppinglist_back.Dtos.RelatedUsersDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class RelatedUsersProfile : Profile
{
    public RelatedUsersProfile()
    {
        CreateMap<CreateRelatedUsersDto, RelatedUsers>();
        CreateMap<RelatedUsers, ReadRelatedUsersDto>();
    }

}
