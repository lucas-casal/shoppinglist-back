using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.RelationProductsListsDto;
using Shoppinglist_back.Services;

namespace Shoppinglist_back.Controllers;

[Controller]
[Route("[Controller]")]
public class RelationProductsListsController : ControllerBase
{
    private RelationProductsListsService _relationProductsListsService;

    public RelationProductsListsController(RelationProductsListsService relationProductsListsService)
    {
        _relationProductsListsService = relationProductsListsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromHeader(Name = "userId")] string userId, [FromBody] CreateRelationProductsListsDto dto)
    {
        var relation = await _relationProductsListsService.Create(userId, dto);
        return CreatedAtAction(nameof(GetOne), new { shoppingListId = relation.ShoppingListId, productId = relation.ProductId }, relation);
    }

    [HttpGet("{shoppingListId}/{productId}")]
    public async Task<IActionResult> GetOne(int shoppingListId, int productId)
    {
        var relation = await _relationProductsListsService.GetOne(shoppingListId, productId);
        return Ok(relation);
    }

    [HttpPatch("{shoppingListId}/{productId}/Description")]
    public async Task<IActionResult> Update([FromHeader(Name ="userId")] string userId, int shoppingListId, int productId, [FromBody] UpdateDescriptionRelationProductsListsDto dto)
    {
        dto.ShoppingListId = shoppingListId;
        dto.ProductId = productId;
        var relation = await _relationProductsListsService.UpdateDescription(userId, dto);
        return Ok(relation);
    }

    [HttpPatch("{shoppingListId}/{productId}/Wanted")]
    public async Task<IActionResult> Update([FromHeader(Name = "userId")] string userId, int shoppingListId, int productId, [FromBody] UpdateWantedRelationProductsListsDto dto)
    {
        dto.ShoppingListId = shoppingListId;
        dto.ProductId = productId;
        var relation = await _relationProductsListsService.UpdateQuantityWanted(userId, dto);
        return Ok(relation);
    }

    [HttpPatch("{shoppingListId}/{productId}/Bought")]
    public async Task<IActionResult> Update([FromHeader(Name = "userId")] string userId, int shoppingListId, int productId, [FromBody] UpdateBoughtRelationProductsListsDto dto)
    {
        dto.ShoppingListId = shoppingListId;
        dto.ProductId = productId;
        var relation = await _relationProductsListsService.UpdateQuantityBought(userId, dto);
        return Ok(relation);
    }

    [HttpDelete("{shoppingListId}/{productId}")]
    public async Task<IActionResult> Delete([FromHeader(Name="userId")] string userId, int shoppingListId, int productId)
    {
        await _relationProductsListsService.Delete(userId, new DeleteRelationProductsListsDto { ShoppingListId = shoppingListId, ProductId = productId});
        return NoContent();
    }
}
