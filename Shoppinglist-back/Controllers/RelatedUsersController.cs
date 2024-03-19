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

            return CreatedAtAction(nameof(GetOneById), new {IdA = relatedUsers.UserAId, IdB = relatedUsers.UserBId}, JsonSerializer.Serialize(relatedUsers));
        }

       [HttpGet("{IdA}/{IdB}")]
        public async Task<IActionResult> GetOneById(string IdA, string IdB)
        {
            var relation = await _relatedUsersService.GetOneById(new GetRelatedUsersDto {UserAId = IdA, UserBId = IdB});

            return Ok(JsonSerializer.Serialize(relation));
        }
       
    }
}
