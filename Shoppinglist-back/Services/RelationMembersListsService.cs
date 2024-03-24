using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Models;
using System.Text.Json;

namespace Shoppinglist_back.Services;

public class RelationMembersListsService
{
    private IMapper _mapper;
    private ShoppinglisterContext _context;

    public RelationMembersListsService(IMapper mapper, ShoppinglisterContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReadRelationMembersListsCompleteDto> Create(string userId, CreateRelationMembersListsDto dto)
    {
        var admin = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == userId);

        if (admin is null || admin?.IsAdmin is false) throw new UnauthorizedAccessException("O usuário que requisitou isso, não é um administrador dessa lista!");

        var shoppingList = await _context.ShoppingList.FirstOrDefaultAsync(sl => sl.Id == dto.ShoppingListId);

        if (shoppingList is null) throw new InvalidOperationException("Essa lista não existe!");

        var relation = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => (rml.UserId == dto.UserId) && (rml.ShoppingListId == dto.ShoppingListId));

        if (relation is not null) throw new InvalidOperationException("esse usuário já é membro dessa lista!");

        var newMember = _mapper.Map<RelationMembersLists>(dto);
        _context.RelationMembersLists.Add(newMember);
        await _context.SaveChangesAsync();

        var newRelation = _context.RelationMembersLists.Include(rml => rml.User).FirstOrDefault(rml => rml.UserId == dto.UserId && rml.ShoppingListId == dto.ShoppingListId);

        return _mapper.Map<ReadRelationMembersListsCompleteDto>(newRelation);
    }
    public async Task CreateMany(IEnumerable<Member> members, int listId) 
    {

        foreach (var member in members)
        {
            CreateRelationMembersListsDto newMemberDto = new CreateRelationMembersListsDto
            {
                ShoppingListId = listId,
                UserId = member.Id,
                IsAdmin = member.IsAdmin
            };

            var newMember = _mapper.Map<RelationMembersLists>(newMemberDto);
            await _context.RelationMembersLists.AddAsync(newMember);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<ReadRelationMembersListsDto> GetOne(string userId, int shoppingListId)
    {
        var relation = await _context.RelationMembersLists.Include(rml => rml.User).Include(rml => rml.ShoppingList)
                                                          .FirstOrDefaultAsync(rml => rml.UserId == userId && rml.ShoppingListId == shoppingListId);

        if (relation is null) throw new InvalidOperationException("Não existe uma relação entre esse usuário e essa lista!");

        var readRelation = new ReadRelationMembersListsDto
        {
            User = relation.User.Name,
            ShoppingList = relation.ShoppingList.Title,
            IsAdmin = relation.IsAdmin
        };

        return readRelation;
    }

    public async Task<ICollection<ReadRelationMembersListsCompleteDto>> GetAll(string? userId, int? shoppingListId)
    {
        var relations = await _context.RelationMembersLists.Include(rml => rml.User).Include(rml => rml.ShoppingList)
                                                        .Select(rml => new ReadRelationMembersListsCompleteDto
                                                        {
                                                            ShoppingListId = rml.ShoppingListId,
                                                            Title = rml.ShoppingList.Title,
                                                            UserId = rml.UserId,
                                                            UserName = rml.User.Name,
                                                            IsAdmin = rml.IsAdmin
                                                        })
                                                        .Where(rml => rml.UserId == userId || rml.ShoppingListId == shoppingListId)
                                                        .OrderBy(rml => rml.ShoppingListId)
                                                        .ToListAsync();
        return relations;
    }

    public async Task<ReadRelationMembersListsDto> UpdateRole(string userId, UpdateRelationMembersListsDto dto)
    {
        var admin = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == userId);

        if (admin is null || admin?.IsAdmin is false) throw new UnauthorizedAccessException("O usuário que requisitou isso, não é um administrador dessa lista!");

        var relation = await _context.RelationMembersLists.Include(rml => rml.User).Include(rml => rml.ShoppingList)
                                                          .FirstOrDefaultAsync(rml => rml.UserId == dto.UserId && rml.ShoppingListId == dto.ShoppingListId);

        if (relation is null) throw new InvalidOperationException("Não existe uma relação entre esse usuário e essa lista!");

        relation.IsAdmin = dto.IsAdmin;
        await _context.SaveChangesAsync();

        var readRelation = new ReadRelationMembersListsDto
        {
            User = relation.User.Name,
            ShoppingList = relation.ShoppingList.Title,
            IsAdmin = relation.IsAdmin
        };

        return readRelation;
    }

    public async Task DeleteOne(string userId, DeleteRelationMembersListsDto dto)
    {
        var admin = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == userId);

        if (admin is null || admin?.IsAdmin is false) throw new UnauthorizedAccessException("O usuário que requisitou isso, não é um administrador dessa lista!");

        var relation = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == dto.UserId && rml.ShoppingListId == dto.ShoppingListId);

        if (relation is null) throw new InvalidOperationException("Não existe uma relação entre esse usuário e essa lista!");

        _context.RelationMembersLists.Remove(relation);

        await _context.SaveChangesAsync();
    }
}
