using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.ShoppingListDtos;
using Shoppinglist_back.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    public async Task<IActionResult> CreateShoppinglist([FromBody] CreateShoppingListDto listDto, [FromHeader(Name = "UserId")] string creatorId)
    {
        
        var shoppingList = await _shoppingListService.Create(listDto, creatorId);

        await _relationMembersListsService.CreateMany(listDto.Members, shoppingList.Id);
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };


        return CreatedAtAction(nameof(GetShoppingListById), new { ListId = shoppingList.Id }, shoppingList);
    }


    [HttpGet("{listId}")]
    public async Task<IActionResult> GetShoppingListById(int listId)
    {

        var shoppingList = await _shoppingListService.GetOne(listId);
        if (shoppingList == null)
        {
            return NotFound();
        }

       
        return Ok(shoppingList);
        
    }

    [HttpGet("byUser/{userId}")]
    public async Task<IActionResult> GetUsersListsByUserId(string userId)
    {

        var shoppingList = await _shoppingListService.GetAllFromUser(userId);
        if (shoppingList == null)
        {
            return NotFound();
        }


        return Ok(shoppingList);

    }

    [HttpDelete("{listId}")]
    public async Task<IActionResult> DeleteOne(int listId)
    {

        await _shoppingListService.DeleteOne(listId);

        return NoContent();

    }

    [HttpPut("{listId}")]
    public async Task<IActionResult> UpdateTitle(int listId, [FromBody] UpdateShoppingListDto newShoppingList)
    {

        var updatedShoppingList = await _shoppingListService.UpdateTitle(listId, newShoppingList);

        return Ok(updatedShoppingList);

    }
}
