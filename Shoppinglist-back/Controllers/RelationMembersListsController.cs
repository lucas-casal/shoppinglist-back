using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Services;

namespace Shoppinglist_back.Controllers;

[Controller]
[Route("[Controller]")]
public class RelationMembersListsController : ControllerBase
{
    private RelationMembersListsService _relationMembersListsServer;

    public RelationMembersListsController(RelationMembersListsService relationMembersListsServer)
    {
        _relationMembersListsServer = relationMembersListsServer;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRelationMembersListsDto dto, [FromHeader(Name = "userId")] string userId)
    {
        var relation = await _relationMembersListsServer.Create(userId, dto);
        return CreatedAtAction(nameof(GetOne), new { userId = relation.UserId, shoppingListId = relation.ShoppingListId }, relation);
    }

    [HttpGet("{userId}/{shoppingListId}")]
    public async Task<IActionResult> GetOne(string userId, int shoppingListId)
    {
        var relation = await _relationMembersListsServer.GetOne(userId, shoppingListId);
        return Ok(relation);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery(Name = "userId")] string userId, [FromQuery(Name = "shoppingListId")] int shoppingListId)
    {
        var relation = await _relationMembersListsServer.GetAll(userId, shoppingListId);
        return Ok(relation);
    }

    [HttpPut("{memberId}/{shoppingListId}")]
    public async Task<IActionResult> UpdateRole(string memberId, int shoppingListId, [FromBody] UpdateRelationMembersListsDto dto, [FromHeader(Name = "userId")] string userId)
    {
        var relation = await _relationMembersListsServer.UpdateRole(userId, new UpdateRelationMembersListsDto { UserId = memberId, ShoppingListId = shoppingListId, IsAdmin = dto.IsAdmin });
        return Ok(relation);
    }

    [HttpDelete("{memberId}/{shoppingListId}")]
    public async Task<IActionResult> DeleteOne(string memberId, int shoppingListId, [FromHeader(Name = "userId")] string userId)
    {
        await _relationMembersListsServer.DeleteOne(userId, new DeleteRelationMembersListsDto { ShoppingListId = shoppingListId, UserId = memberId });
        return NoContent();
    }

}
