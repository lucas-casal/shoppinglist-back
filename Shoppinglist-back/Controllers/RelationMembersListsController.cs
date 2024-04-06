using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Services;

namespace Shoppinglist_back.Controllers;

[Controller]
[Route("[Controller]")]
[Authorize(Policy = "HasToken")]
public class RelationMembersListsController : ControllerBase
{
    private RelationMembersListsService _relationMembersListsServer;

    public RelationMembersListsController(RelationMembersListsService relationMembersListsServer)
    {
        _relationMembersListsServer = relationMembersListsServer;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token, [FromBody] CreateRelationMembersListsDto dto)
    {
        var relation = await _relationMembersListsServer.Create(token, dto);
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
    public async Task<IActionResult> UpdateRole([FromHeader(Name = "Authorization")] string token, string memberId, int shoppingListId, [FromBody] UpdateRelationMembersListsDto dto)
    {
        var relation = await _relationMembersListsServer.UpdateRole(token, new UpdateRelationMembersListsDto { UserId = memberId, ShoppingListId = shoppingListId, IsAdmin = dto.IsAdmin });
        return Ok(relation);
    }

    [HttpDelete("{memberId}/{shoppingListId}")]
    public async Task<IActionResult> DeleteOne([FromHeader(Name = "Authorization")] string token, string memberId, int shoppingListId)
    {
        await _relationMembersListsServer.DeleteOne(token, new DeleteRelationMembersListsDto { ShoppingListId = shoppingListId, UserId = memberId });
        return NoContent();
    }

}
