using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.JoinListRequestDtos;
using Shoppinglist_back.Services;

namespace Shoppinglist_back.Controllers;

[Controller]
[Route("[Controller]")]
[Authorize(Policy = "HasToken")]
public class JoinListRequestController : ControllerBase
{
    private JoinListRequestService _joinListRequestService;

    public JoinListRequestController(JoinListRequestService joinListRequestService)
    {
        _joinListRequestService = joinListRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromHeader(Name="Authorization")] string token, [FromBody] CreateJoinListRequestDto dto) 
    {
        var request = await _joinListRequestService.Create(token, dto);

        return CreatedAtAction(nameof(GetOne), new { userId = request.UserId, shoppingListId = request.ShoppingListId } , request);
    }

    [HttpGet("{userId}/{shoppingListId}")]
    public async Task<IActionResult> GetOne([FromHeader(Name = "Authorization")] string token, string userId, int shoppingListId)
    {
        var request = await _joinListRequestService.GetOne(token, userId, shoppingListId);

        return Ok(request);
    }

    [HttpGet("shoppinglist/{shoppingListId}")]
    public async Task<IActionResult> GetAllByListId([FromHeader(Name = "Authorization")] string token, int shoppingListId)
    {
        var requests = await _joinListRequestService.GetAllByListId(token, shoppingListId);
        return Ok(requests);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllByUserId([FromHeader(Name = "Authorization")] string token, string userId)
    {
        var requests = await _joinListRequestService.GetAllByUserId(token, userId);
        return Ok(requests);
    }

    [HttpPut("{userId}/{shoppingListId}")]
    public async Task<IActionResult> AnswerRequest([FromHeader(Name = "Authorization")] string token, string userId, int shoppingListId, [FromBody] AnswerJoinListRequestDto dto)
    {
        var response = await _joinListRequestService.AnswerRequest(token, userId, shoppingListId, dto);

        return Ok(response);
    }
}
