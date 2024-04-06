using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shoppinglist_back.Dtos.RelatedUsersRequestDtos;
using Shoppinglist_back.Models;
using Shoppinglist_back.Services;
using System.Text.Json;

namespace Shoppinglist_back.Controllers;

[Controller]
[Route("[Controller]")]
public class RelatedUsersRequestController : ControllerBase
{
    private RelatedUsersRequestService _relatedUsersRequestService;

    public RelatedUsersRequestController(RelatedUsersRequestService relatedUsersRequestService)
    {
        _relatedUsersRequestService = relatedUsersRequestService;
    }

    [HttpPost]
    [Authorize(Policy = "HasToken")]
    public async Task<IActionResult> Create([FromHeader(Name = "Authorization")] string token, [FromBody] CreateRelatedUsersRequestDto dto)
    {
        var request = await _relatedUsersRequestService.Create(token, dto);
        return CreatedAtAction(nameof(GetOne), new {IdA = dto.UserAId, IdB = dto.UserBId}, request);
    }

    [HttpGet("{IdA}/{IdB}")]
    public async Task<IActionResult> GetOne(string IdA, string IdB)
    {
        var request = await _relatedUsersRequestService.GetOne(IdA, IdB);

        return Ok(request);
    }

    [HttpGet("{UserId}")]
    public async Task<IActionResult> GetAllByUserId(string UserId)
    {
        var requests = await _relatedUsersRequestService.GetAllByUserId(UserId);
        
        return Ok(requests);
    }

    [HttpPut("{IdA}")]
    [Authorize(Policy = "HasToken")]
    public async Task<IActionResult> Answer([FromHeader(Name ="Authorization")] string token, string IdA, [FromBody] AnswerRelatedUsersRequestDto dto, [FromHeader(Name = "UserId")] string UserId)
    {
        var response = new RelatedUsersRequest
        {
            UserAId = IdA,
            UserBId = UserId,
            Approved = dto.Approved
        };

            await _relatedUsersRequestService.Answer(token, response);
            return Ok(response);

    }

    [HttpDelete("{UserAId}/{UserBId}")]
    public async Task<IActionResult> DeleteOne(string UserAId, string UserBId)
    {
        var dto = new DeleteRelatedUsersRequestDto 
        { 
            UserAId = UserAId, 
            UserBId = UserBId 
        };

        await _relatedUsersRequestService.DeleteOne(dto);
        return NoContent();
    }

}


