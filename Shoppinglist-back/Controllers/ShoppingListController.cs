using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Dtos.ShoppingListDtos;
using Shoppinglist_back.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Shoppinglist_back.Controllers;

[ApiController]
[Route("[Controller]")]
public class ShoppingListController : ControllerBase
{
    private ShoppingListService _shoppingListService;
    private RelationMembersListsService _relationMembersListsService;

    public ShoppingListController(ShoppingListService shoppingListService, RelationMembersListsService relationMembersListsService)
    {
        _shoppingListService = shoppingListService;
        _relationMembersListsService = relationMembersListsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShoppinglist(CreateShoppingListDto listDto)
    {
        
        var shoppingList = await _shoppingListService.Create(listDto);

        await _relationMembersListsService.Create(listDto.Members, shoppingList.Id);


        return CreatedAtAction(nameof(GetShoppingListById), new { Id = shoppingList.Id }, JsonSerializer.Serialize(shoppingList));
    }


    [HttpGet("{listId}")]
    public async Task<IActionResult> GetShoppingListById(int listId)
    {

        var shoppingList = await _shoppingListService.GetOne(listId);
        if (shoppingList == null)
        {
            return NotFound();
        }

       
        return Ok(JsonSerializer.Serialize(shoppingList));
        
    }

    [HttpGet("byUser/{userId}")]
    public async Task<IActionResult> GetUsersListsByUserId(string userId)
    {

        var shoppingList = await _shoppingListService.GetAllFromUser(userId);
        if (shoppingList == null)
        {
            return NotFound();
        }


        return Ok(JsonSerializer.Serialize(shoppingList));

    }

    [HttpDelete("{listId}")]
    public async Task<IActionResult> DeleteOne(int listId)
    {

        await _shoppingListService.DeleteOne(listId);

        return NoContent();

    }
}
