using AutoMapper;
using Shoppinglist_back.Dtos.RelatedUsersRequestDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class RelatedUsersRequestProfile : Profile
{
    public RelatedUsersRequestProfile()
    {
        CreateMap<CreateRelatedUsersRequestDto, RelatedUsersRequest>();
        CreateMap<RelatedUsersRequest, ReadRelatedUsersRequestDto>();
    }
}
