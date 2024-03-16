using AutoMapper;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Services;

public class RelationMembersListsService
{
    private IMapper _mapper;
    private ShoppinglisterContext _context;

    public RelationMembersListsService(IMapper mapper, ShoppinglisterContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task Create(IEnumerable<Member> members, int listId) 
    {

        foreach (var member in members)
        {
            CreateRelationMembersListsDto newMemberDto = new CreateRelationMembersListsDto
            {
                ShoppingListId = listId,
                UserId = member.Id.ToString(),
                IsAdmin = member.IsAdmin
            };

            var newMember = _mapper.Map<RelationMembersLists>(newMemberDto);
            await _context.RelationMembersLists.AddAsync(newMember);
            await _context.SaveChangesAsync();
        }
    }


}
