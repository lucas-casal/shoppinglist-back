using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.RelatedUsersDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Services;

public class RelatedUsersService
{
    private IMapper _mapper;
    private UserContext _context;
    public RelatedUsersService(IMapper mapper, UserContext userContext)
    {
        _mapper = mapper;
        _context = userContext;
    }

    public async Task<RelatedUsers> Create(CreateRelatedUsersDto dto)
    {
        var relatedUsers = _mapper.Map<RelatedUsers>(dto);

        await _context.RelatedUsers.AddAsync(relatedUsers);
        _context.SaveChanges();

        return relatedUsers;
    }

    public async Task<RelatedUsers> GetOneById(GetRelatedUsersDto ids)
    {
        var relation = await _context.RelatedUsers.FirstOrDefaultAsync(ru => 
                                (ru.UserAId == ids.UserAId || ru.UserAId == ids.UserBId)
                                &&
                                (ru.UserBId == ids.UserAId || ru.UserBId == ids.UserBId)
                                );

        if (relation is null) throw new ArgumentException("Relation not found");

        return relation;
    }
}
