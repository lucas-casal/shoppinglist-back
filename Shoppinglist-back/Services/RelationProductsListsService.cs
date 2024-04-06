using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.RelationProductsListsDto;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Services;

public class RelationProductsListsService
{
    private IMapper _mapper;
    private ShoppinglisterContext _context;

    public RelationProductsListsService(IMapper mapper, ShoppinglisterContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReadRelationProductsListsDto> Create(string token, CreateRelationProductsListsDto dto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var adminId = session.UserId;
        var user = await _context.Users.Include(u => u.RelationMembersLists)
                                        .Select(u => new
                                        {
                                            Id = u.Id,
                                            Name = u.Name,
                                            Role = u.RelationMembersLists.Select(rml => new 
                                            {
                                                ShoppingListId = rml.ShoppingListId,
                                                Title = rml.ShoppingList.Title,
                                                IsAdmin = rml.IsAdmin
                                            }).Where(rml => rml.ShoppingListId == dto.ShoppingListId).ToList()
                                        })
                                        .FirstOrDefaultAsync(u => u.Id ==  adminId);
        if (user is null || user.Role.Count == 0 || user.Role[0].IsAdmin is not true) throw new UnauthorizedAccessException("Esse usuário não é administrador dessa lista!");

        var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == dto.ProductId);

        if (product is null) throw new NullReferenceException("Esse produto não foi encontrado na nossa lista!");

        var newRelation = _mapper.Map<RelationProductsLists>(dto);
        newRelation.Description = newRelation.Description.Normalize();
        _context.RelationProductsLists.Add(newRelation);
        await _context.SaveChangesAsync();

        var ReadNewRelation = _mapper.Map<ReadRelationProductsListsDto>(newRelation);
        ReadNewRelation.Title = user.Role[0].Title;

        return ReadNewRelation;
    }

    public async Task<ReadRelationProductsListsDto> GetOne(int shoppingListId, int productId)
    {
        var relation = await _context.RelationProductsLists.Include(rpl => rpl.Product).Include(rpl => rpl.ShoppingList)
                                                           .FirstOrDefaultAsync(rpl => rpl.ProductId == productId && rpl.ShoppingListId == shoppingListId);

        if (relation is null) throw new NullReferenceException("Esse item não está nessa lista!");

        return _mapper.Map<ReadRelationProductsListsDto>(relation);
    }

    public async Task<ReadRelationProductsListsDto> UpdateDescription(string token, UpdateDescriptionRelationProductsListsDto dto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var adminId = session.UserId;
        var user = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == adminId && rml.ShoppingListId == dto.ShoppingListId);

        if (user is null || user.IsAdmin is false) throw new InvalidOperationException("O usuário não está autorizado a realizar essa alteração!");

        var relation = await _context.RelationProductsLists.Include(rpl => rpl.Product).Include(rpl => rpl.ShoppingList)
                                                           .FirstOrDefaultAsync(rpl => rpl.ProductId == dto.ProductId && rpl.ShoppingListId == dto.ShoppingListId);

        if (relation is null) throw new NullReferenceException("Esse item não está nessa lista!");

        relation.Description = dto.Description.Normalize();

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadRelationProductsListsDto>(relation);
    }

    public async Task<ReadRelationProductsListsDto> UpdateQuantityWanted(string token, UpdateWantedRelationProductsListsDto dto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var adminId = session.UserId;
        var user = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == adminId && rml.ShoppingListId == dto.ShoppingListId);

        if (user is null || user.IsAdmin is false) throw new InvalidOperationException("O usuário não está autorizado a realizar essa alteração!");

        var relation = await _context.RelationProductsLists.Include(rpl => rpl.Product).Include(rpl => rpl.ShoppingList)
                                                           .FirstOrDefaultAsync(rpl => rpl.ProductId == dto.ProductId && rpl.ShoppingListId == dto.ShoppingListId);

        if (relation is null) throw new NullReferenceException("Esse item não está nessa lista!");

        relation.QuantityWanted = dto.QuantityWanted;

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadRelationProductsListsDto>(relation);
    }

    public async Task<ReadRelationProductsListsDto> UpdateQuantityBought(string token, UpdateBoughtRelationProductsListsDto dto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var userId = session.UserId;
        var user = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == userId && rml.ShoppingListId == dto.ShoppingListId);

        if (user is null) throw new InvalidOperationException("O usuário não está autorizado a realizar essa alteração!");

        var relation = await _context.RelationProductsLists.Include(rpl => rpl.Product).Include(rpl => rpl.ShoppingList)
                                                           .FirstOrDefaultAsync(rpl => rpl.ProductId == dto.ProductId && rpl.ShoppingListId == dto.ShoppingListId);

        if (relation is null) throw new NullReferenceException("Esse item não está nessa lista!");

        relation.QuantityBought = dto.QuantityBought;

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadRelationProductsListsDto>(relation);
    }

    public async Task Delete(string token, DeleteRelationProductsListsDto dto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var adminId = session.UserId;
        var user = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == adminId && rml.ShoppingListId == dto.ShoppingListId);

        if (user is null || user.IsAdmin is false) throw new InvalidOperationException("O usuário não está autorizado a realizar essa alteração!");

        var relation = await _context.RelationProductsLists.FirstOrDefaultAsync(rpl => rpl.ProductId == dto.ProductId && rpl.ShoppingListId == dto.ShoppingListId);

        if (relation is null) throw new NullReferenceException("Esse item não está nessa lista!");

        _context.RelationProductsLists.Remove(relation);
        await _context.SaveChangesAsync();
    }
}
