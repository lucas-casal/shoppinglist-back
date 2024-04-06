using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Dtos.RelationProductsListsDto;
using Shoppinglist_back.Dtos.ShoppingListDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Services;

public class ShoppingListService
{
    private IMapper _mapper;
    private ShoppinglisterContext _context;

    public ShoppingListService(IMapper mapper, ShoppinglisterContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ReadShoppingListDto> Create(string token, CreateShoppingListDto listDto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var userId = session.UserId;
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) throw new NullReferenceException("Não foi possível encontrar o usuário para criar essa lista");

        var newList = _mapper.Map<ShoppingList>(listDto);
        
        _context.ShoppingList.Add(newList);
        await _context.SaveChangesAsync();
        var readList = await GetOne(newList.Id);
        return readList;
    }

    public async Task<ReadShoppingListDto> GetOne(int id)
    {
        var shoppingList = await _context.ShoppingList.Include(sl => sl.RelationMembersLists)
                                  .Select(sl => new ReadShoppingListDto
                                  {
                                      Id = sl.Id,
                                      Title = sl.Title,
                                      Members = sl.RelationMembersLists.Where(rml => rml.ShoppingListId == sl.Id).Select(rml => new ReadRelationMembersListsThroughListDto
                                      {
                                          UserId = rml.UserId,
                                          Name = rml.User.Name,
                                          IsAdmin = rml.IsAdmin
                                      }).OrderBy(rml => rml.Name).ToList(),
                                      Products = sl.RelationProductsLists.Where(rpl => rpl.ShoppingListId == sl.Id).Select(rpl => new ReadRelationProductsListsThroughListIdDto
                                      {
                                          Id = rpl.ProductId,
                                          Name = rpl.Product.Name,
                                          Description = rpl.Description,
                                          QuantityWanted = rpl.QuantityWanted,
                                          QuantityBought = rpl.QuantityBought
                                      }).ToList()
                                  })
                                  .FirstOrDefaultAsync(sl => sl.Id == id);
        if (shoppingList == null) throw new NullReferenceException("não foi possível encontrar essa lista");

        return shoppingList;
    }

    public async Task<List<ReadShoppingListDto>> GetAllFromUser(string id)
    {
        var shoppingLists = await _context.ShoppingList
        .Where(sl => sl.RelationMembersLists.Any(rml => rml.UserId == id))
        .Include(sl => sl.RelationMembersLists)
        .Select(sl => new ReadShoppingListDto
        {
            Id = sl.Id,
            Title = sl.Title,
            Members = sl.RelationMembersLists
                .Select(rml => new ReadRelationMembersListsThroughListDto
                {
                    UserId = rml.UserId,
                    Name = rml.User.Name,
                    IsAdmin = rml.IsAdmin
                })
                .ToList()
        })
        .ToListAsync();

        return shoppingLists;
    }

    public async Task DeleteOne(string token, int id)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var userId = session.UserId;
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) throw new NullReferenceException("Não foi possível encontrar o usuário");

        var shoppingList = await _context.ShoppingList.Include(sl => sl.RelationMembersLists).FirstOrDefaultAsync(sl => sl.Id == id);

        if (shoppingList is null)
        {
            throw new ArgumentException("Shopping list not found.");
        }

        if (shoppingList.RelationMembersLists.Any(rml => rml.IsAdmin is true && rml.UserId == userId) is false) throw new UnauthorizedAccessException("Esse usuário não é admin dessa lista!");

        _context.ShoppingList.Remove(shoppingList);
        await _context.SaveChangesAsync();
    }

    public async Task<ReadShoppingListDto> UpdateTitle(string token, int id, UpdateShoppingListDto newShoppingList)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var userId = session.UserId;
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) throw new NullReferenceException("Não foi possível encontrar o usuário");

        var shoppingList = await _context.ShoppingList.Include(sl => sl.RelationMembersLists).FirstOrDefaultAsync(sl => sl.Id == id);
        
        if (shoppingList is null) throw new ArgumentException("Shopping list not found.");

        if (shoppingList.RelationMembersLists.Any(rml => rml.IsAdmin is true && rml.UserId == userId) is false) throw new UnauthorizedAccessException("Esse usuário não é admin dessa lista!");

        shoppingList.Title = newShoppingList.Title;
        var completeInfo = _mapper.Map<ReadShoppingListDto>(shoppingList);
        await _context.SaveChangesAsync();



        return completeInfo;
    }
}
