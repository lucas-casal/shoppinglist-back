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

    public async Task<ReadRelatedUsersDto> GetOneById(CreateRelatedUsersDto ids)
    {
        var relation = await _context.RelatedUsers.FirstOrDefaultAsync(ru => 
                                (ru.UserAId == ids.UserAId || ru.UserAId == ids.UserBId)
                                &&
                                (ru.UserBId == ids.UserAId || ru.UserBId == ids.UserBId)
                                );

        if (relation is null) throw new ArgumentException("Relation not found");
        var readRelation = _mapper.Map<ReadRelatedUsersDto>(relation);

        return readRelation;
    }

    public async Task<IEnumerable<ReadRelatedUsersByUserIdDto>> GetAllByUserId(string UserId)
    {
        var relations = await _context.RelatedUsers
                                .Where(ru => (ru.UserAId == UserId || ru.UserBId == UserId))
                                .Select(ru => new ReadRelatedUsersByUserIdDto
                                    {
                                        UserId = UserId == ru.UserAId ? ru.UserBId : ru.UserAId,
                                        UserName = UserId == ru.UserAId ? ru.UserB.Name : ru.UserA.Name,
                                        Nickname = UserId == ru.UserAId ? ru.NicknameB : ru.NicknameA
                                    }
                                ).ToListAsync();
                                

        return relations;
    }

    public async Task<ReadRelatedUsersByUserIdDto> ChangeNickname(UpdateNicknameRelatedUsersDto dto)
    {
        var relation = await _context.RelatedUsers
                                .Include(ru => ru.UserA)
                                .Include(ru => ru.UserB)
                                .FirstOrDefaultAsync(ru =>
                                (ru.UserAId == dto.UserId || ru.UserBId == dto.UserId)
                                &&
                                (ru.UserAId == dto.RelatedId || ru.UserBId == dto.RelatedId)
                                );
        if (relation is null) throw new ArgumentException("Não foi encontrada uma relação entre esses usuários");

        if (relation.UserAId == dto.RelatedId) relation.NicknameA = dto.Nickname;
        
        if (relation.UserBId == dto.RelatedId) relation.NicknameB = dto.Nickname;

        await _context.SaveChangesAsync();

        var readRelation = new ReadRelatedUsersByUserIdDto
        {
            UserId = dto.UserId == relation.UserAId ? relation.UserBId ?? relation.UserAId : relation.UserAId ?? relation.UserBId,
            UserName = dto.UserId == relation.UserAId ? relation.UserB?.Name ?? relation.UserA?.Name : relation.UserA?.Name ?? relation.UserB?.Name,
            Nickname = dto.UserId == relation.UserAId ? relation.NicknameB ?? relation.NicknameA : relation.NicknameA ?? relation.NicknameB
        };
        return readRelation;
    }

    public async Task DeleteOne(DeleteRelatedUsersDto ids)
    {
        var relation = await _context.RelatedUsers.FirstOrDefaultAsync(ru =>
                               (ru.UserAId == ids.UserAId || ru.UserAId == ids.UserBId)
                               &&
                               (ru.UserBId == ids.UserAId || ru.UserBId == ids.UserBId)
                               );

        if (relation is null) throw new ArgumentException("Relation not found");

        _context.RelatedUsers.Remove(relation);
        _context.SaveChanges();
    }
}
