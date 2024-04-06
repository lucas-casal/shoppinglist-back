using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "HasToken")]
    public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token, [FromBody] CreateRelationProductsListsDto dto)
    {
        var relation = await _relationProductsListsService.Create(token, dto);
        return CreatedAtAction(nameof(GetOne), new { shoppingListId = relation.ShoppingListId, productId = relation.ProductId }, relation);
    }

    [HttpGet("{shoppingListId}/{productId}")]
    public async Task<IActionResult> GetOne(int shoppingListId, int productId)
    {
        var relation = await _relationProductsListsService.GetOne(shoppingListId, productId);
        return Ok(relation);
    }

    [Authorize(Policy = "HasToken")]
    [HttpPatch("{shoppingListId}/{productId}/Description")]
    public async Task<IActionResult> Update([FromHeader(Name = "Authorization")] string token, int shoppingListId, int productId, [FromBody] UpdateDescriptionRelationProductsListsDto dto)
    {
        dto.ShoppingListId = shoppingListId;
        dto.ProductId = productId;
        var relation = await _relationProductsListsService.UpdateDescription(token, dto);
        return Ok(relation);
    }

    [Authorize(Policy = "HasToken")]
    [HttpPatch("{shoppingListId}/{productId}/Wanted")]
    public async Task<IActionResult> Update([FromHeader(Name = "Authorization")] string token, int shoppingListId, int productId, [FromBody] UpdateWantedRelationProductsListsDto dto)
    {
        dto.ShoppingListId = shoppingListId;
        dto.ProductId = productId;
        var relation = await _relationProductsListsService.UpdateQuantityWanted(token, dto);
        return Ok(relation);
    }

    [Authorize(Policy = "HasToken")]
    [HttpPatch("{shoppingListId}/{productId}/Bought")]
    public async Task<IActionResult> Update([FromHeader(Name = "Authorization")] string token, int shoppingListId, int productId, [FromBody] UpdateBoughtRelationProductsListsDto dto)
    {
        dto.ShoppingListId = shoppingListId;
        dto.ProductId = productId;
        var relation = await _relationProductsListsService.UpdateQuantityBought(token, dto);
        return Ok(relation);
    }

    [Authorize(Policy = "HasToken")]
    [HttpDelete("{shoppingListId}/{productId}")]
    public async Task<IActionResult> Delete([FromHeader(Name = "Authorization")] string token, int shoppingListId, int productId)
    {
        await _relationProductsListsService.Delete(token, new DeleteRelationProductsListsDto { ShoppingListId = shoppingListId, ProductId = productId});
        return NoContent();
    }
}
