using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.RelatedUsersDtos;
using Shoppinglist_back.Services;
using System.Text.Json;

namespace Shoppinglist_back.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RelatedUsersController : ControllerBase
    {
        private RelatedUsersService _relatedUsersService;

        public RelatedUsersController(RelatedUsersService relatedUsersService)
        {
            _relatedUsersService = relatedUsersService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRelatedUsersDto dto)
        {
            var relatedUsers = await _relatedUsersService.Create(dto);

            return CreatedAtAction(nameof(GetOneById), new { IdA = relatedUsers.UserAId, IdB = relatedUsers.UserBId }, JsonSerializer.Serialize(relatedUsers));
        }

        [HttpGet("{IdA}/{IdB}")]
        public async Task<IActionResult> GetOneById(string IdA, string IdB)
        {
            var relation = await _relatedUsersService.GetOneById(new CreateRelatedUsersDto { UserAId = IdA, UserBId = IdB });

            return Ok(JsonSerializer.Serialize(relation));
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetAllByUserId(string UserId)
        {
            var relation = await _relatedUsersService.GetAllByUserId(UserId);

            return Ok(JsonSerializer.Serialize(relation));
        }

        [HttpPatch("{RelatedId}")]
        public async Task<IActionResult> ChangeNickname(string RelatedId, [FromHeader(Name = "UserId")] string UserId, [FromBody] UpdateNicknameRelatedUsersDto dto)
        {

            var relation = await _relatedUsersService.ChangeNickname(new UpdateNicknameRelatedUsersDto { UserId = UserId, RelatedId = RelatedId, Nickname = dto.Nickname });

            return Ok(JsonSerializer.Serialize(relation)) ;
        }

        [HttpDelete("{IdA}/{IdB}")]
        public async Task<IActionResult> DeleteOne(string IdA, string IdB)
        {
            await _relatedUsersService.DeleteOne(new DeleteRelatedUsersDto { UserAId = IdA, UserBId = IdB });

            return NoContent();
        }

    }
}
