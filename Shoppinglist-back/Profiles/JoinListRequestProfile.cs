using AutoMapper;
using Shoppinglist_back.Dtos.JoinListRequestDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Profiles;

public class JoinListRequestProfile : Profile
{
    public JoinListRequestProfile()
    {
        CreateMap<CreateJoinListRequestDto, JoinListRequest>();
        CreateMap<JoinListRequest, ReadJoinListRequestDto>();
    }
}
