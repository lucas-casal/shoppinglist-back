using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
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
    public async Task<ShoppingList> Create(CreateShoppingListDto listDto)
    {
        var newList = _mapper.Map<ShoppingList>(listDto);
        _context.ShoppingList.Add(newList);
        await _context.SaveChangesAsync();
        return newList;
    }

    public async Task<ReadShoppingListDto> GetOne(int id)
    {
        var shoppingList = await _context.ShoppingList.Include(sl => sl.RelationMembersLists)
                                  .FirstOrDefaultAsync(sl => sl.Id == id);
        var completeInfo = _mapper.Map<ReadShoppingListDto>(shoppingList);
        return completeInfo;
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
            ReadRelationMLDtos = sl.RelationMembersLists
                .Select(rml => new ReadRelationMembersListsThroughListDto
                {
                    UserId = rml.UserId,
                    IsAdmin = rml.IsAdmin
                })
                .ToList()
        })
        .ToListAsync();

        return shoppingLists;
    }

    public async Task DeleteOne(int id)
    {
        var shoppingList = await _context.ShoppingList.FindAsync(id);

        if (shoppingList == null)
        {
            throw new ArgumentException("Shopping list not found.");
        }

        _context.ShoppingList.Remove(shoppingList); // Não é necessário anexar porque a entidade já está sendo rastreada
        await _context.SaveChangesAsync();
    }

    public async Task<ReadShoppingListDto> UpdateTitle(int id, UpdateShoppingListDto newShoppingList)
    {
        
        var shoppingList = await _context.ShoppingList.Include(sl => sl.RelationMembersLists).FirstOrDefaultAsync(sl => sl.Id == id);
        
        if (shoppingList == null) throw new ArgumentException("Shopping list not found.");
      

        shoppingList.Title = newShoppingList.Title;
        var completeInfo = _mapper.Map<ReadShoppingListDto>(shoppingList);
        await _context.SaveChangesAsync();



        return completeInfo;
    }
}
