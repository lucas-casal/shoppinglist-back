using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "HasToken")]
    public async Task<IActionResult> CreateShoppinglist([FromHeader(Name = "Authorization")] string token, [FromBody] CreateShoppingListDto listDto)
    {
        
        var shoppingList = await _shoppingListService.Create(token, listDto);

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
    [Authorize(Policy = "HasToken")]
    public async Task<IActionResult> DeleteOne([FromHeader(Name="Authorization")] string token, int listId)
    {

        await _shoppingListService.DeleteOne(token, listId);

        return NoContent();

    }

    [HttpPut("{listId}")]
    [Authorize(Policy = "HasToken")]
    public async Task<IActionResult> UpdateTitle([FromHeader(Name = "Authorization")] string token, int listId, [FromBody] UpdateShoppingListDto newShoppingList)
    {

        var updatedShoppingList = await _shoppingListService.UpdateTitle(token, listId, newShoppingList);

        return Ok(updatedShoppingList);

    }
}
