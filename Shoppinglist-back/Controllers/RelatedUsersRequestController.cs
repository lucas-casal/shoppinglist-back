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
    public async Task<IActionResult> Create([FromBody] CreateRelatedUsersRequestDto dto)
    {
        var request = await _relatedUsersRequestService.Create(dto);
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
    public async Task<IActionResult> Answer(string IdA, [FromBody] AnswerRelatedUsersRequestDto dto, [FromHeader(Name = "UserId")] string UserId)
    {
        var response = new RelatedUsersRequest
        {
            UserAId = IdA,
            UserBId = UserId,
            Approved = dto.Approved
        };
        try
        {
            await _relatedUsersRequestService.Answer(response);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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


