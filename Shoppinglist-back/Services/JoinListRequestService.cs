using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.JoinListRequestDtos;
using Shoppinglist_back.Dtos.RelatedUsersDtos;
using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Models;
using System.Transactions;

namespace Shoppinglist_back.Services;

public class JoinListRequestService
{
    private IMapper _mapper;
    private ShoppinglisterContext _context;

    public JoinListRequestService(IMapper mapper, ShoppinglisterContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReadJoinListRequestDto> Create(string token, CreateJoinListRequestDto dto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var requestAuthorId = session.UserId;
        var requestAuthor = await _context.Users.FirstOrDefaultAsync(u => u.Id == requestAuthorId);
        if (requestAuthor is null) throw new Exception("usuário requisitante não encontrado!");

        if(dto.Invited is true) 
        { 
            var requestAuthorRelation = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == requestAuthorId && rml.ShoppingListId == dto.ShoppingListId);

            if (requestAuthorRelation is null || requestAuthorRelation.IsAdmin is not true) throw new Exception("Esse usuário não está autorizado a fazer essa ação!");
        }

        var user = dto.Invited is true ? await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId) : requestAuthor;

        if (user is null) throw new Exception("não foi possível encontrar esse usuário para convidá-lo");

        var alreadyRelated = await _context.RelationMembersLists.FirstOrDefaultAsync(rml => rml.UserId == user.Id && rml.ShoppingListId == dto.ShoppingListId);
        if (alreadyRelated is not null) throw new Exception($"{alreadyRelated.UserId}");
        
        var alreadyInvited = await _context.JoinListRequest.FirstOrDefaultAsync(jlr => jlr.UserId == user.Id && jlr.ShoppingListId == dto.ShoppingListId);
        if (alreadyInvited is not null && alreadyInvited.Approved is null) throw new Exception("Esse usuário já está com um convite em aberto para essa lista!");

        if(alreadyInvited is not null && alreadyInvited.Approved is false)
        {
            _context.JoinListRequest.Remove(alreadyInvited);
            await _context.SaveChangesAsync();
        }
        var request = _mapper.Map<JoinListRequest>(dto);
        _context.JoinListRequest.Add(request);
        await _context.SaveChangesAsync();

        var savedRequest = await _context.JoinListRequest.Include(jlr => jlr.User).Include(jlr => jlr.ShoppingList)
                                                         .Select(jlr => new ReadJoinListRequestDto
                                                         {
                                                             ShoppingListId = jlr.ShoppingListId,
                                                             ShoppingListName = jlr.ShoppingList.Title,
                                                             UserId = jlr.User.Id,
                                                             UserName = jlr.User.Name,
                                                             UserEmail = jlr.User.Email!,
                                                             Invited = jlr.Invited,
                                                             Approved = jlr.Approved
                                                         })
                                                         .FirstOrDefaultAsync(jlr => jlr.ShoppingListId == dto.ShoppingListId && jlr.UserId == dto.UserId)
                                                         ;

        return savedRequest!;
    }

    public async Task<ReadJoinListRequestDto> GetOne(string token, string userId, int shoppingListId)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var requestAuthorId = session.UserId;
        var requestAuthor = await _context.Users.FirstOrDefaultAsync(u => u.Id == requestAuthorId);
        
        if (requestAuthor is null) throw new Exception("usuário não encontrado");

        var relation = await _context.JoinListRequest.Include(jlr => jlr.ShoppingList)
                                                     .Select(jlr => new
                                                     {
                                                         ShoppingListId = jlr.ShoppingListId,
                                                         ShoppingListName = jlr.ShoppingList.Title,
                                                         UserId = jlr.UserId,
                                                         UserName = jlr.User.Name,
                                                         UserEmail = jlr.User.Email!,
                                                         Invited = jlr.Invited,
                                                         Approved = jlr.Approved,
                                                         Members = jlr.ShoppingList.RelationMembersLists.Where(rml => rml.ShoppingListId == shoppingListId && rml.UserId == requestAuthorId).ToList()
                                                     })
                                                     .FirstOrDefaultAsync(jlr => jlr.UserId == userId);

        if (relation is null) throw new Exception("Requisição não encontrada");

        if (requestAuthorId != userId)
        {
            if (relation.Members.Count == 0) throw new Exception("Esse usuário não está autorizado a ver essa requisição");
            if (relation.Members.Count > 0 && relation.Members[0].IsAdmin is not true) throw new Exception("Esse usuário não está autorizado a ver essa requisição");
        }

        var response = new ReadJoinListRequestDto
        {
            ShoppingListId = relation.ShoppingListId,
            ShoppingListName = relation.ShoppingListName,
            UserId = relation.UserId,
            UserName = relation.UserName,
            UserEmail = relation.UserEmail,
            Invited = relation.Invited,
            Approved = relation.Approved,
        };

        return response;
    }

    public async Task<ICollection<ReadJoinListRequestsByListId>> GetAllByListId(string token, int shoppingListId)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var requestAuthorId = session.UserId;
        var requestAuthor = await _context.Users.FirstOrDefaultAsync(u => u.Id == requestAuthorId);

        var shoppingList = await _context.ShoppingList.Include(sl => sl.RelationMembersLists)
                                                      .Select(sl => new
                                                      {
                                                          Id = sl.Id,
                                                          Member = sl.RelationMembersLists.FirstOrDefault(rml => rml.UserId == requestAuthorId && sl.Id == rml.ShoppingListId)
                                                      })
                                                      .FirstOrDefaultAsync(sl => sl.Id == shoppingListId);
        if (shoppingList is null) throw new NullReferenceException("Essa lista não foi encontrada!");

        if (shoppingList.Member is null) throw new Exception("Você não é membro dessa lista!");

        var requests = await _context.JoinListRequest.Include(jlr => jlr.User).Include(jlr => jlr.ShoppingList)
                                                     .Where(jlr => jlr.ShoppingListId == shoppingListId)
                                                     .Select(jlr => new ReadJoinListRequestsByListId
                                                     {
                                                         UserId = jlr.User.Id,
                                                         UserEmail = jlr.User.Email!,
                                                         UserName = jlr.User.Name,
                                                         Invited = jlr.Invited,
                                                         Approved = jlr.Approved
                                                     })
                                                     .OrderBy(jlr => jlr.Invited)
                                                     .ToListAsync();
        return requests;
    }

    public async Task<ICollection<ReadJoinListRequestsByUserId>> GetAllByUserId(string token, string userId)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var requestAuthorId = session.UserId;

        if (requestAuthorId != userId) throw new Exception("Você não pode consultar as requisições de outro usuário");
        var requestAuthor = await _context.Users.FirstOrDefaultAsync(u => u.Id ==  userId);

        if (requestAuthor == null) throw new Exception("Usuário inexistente");

        var requests = await _context.JoinListRequest.Include(jlr => jlr.ShoppingList)
                                                     .Where(jlr => jlr.UserId == userId)
                                                     .Select(jlr => new ReadJoinListRequestsByUserId
                                                     {
                                                         ShoppingListId = jlr.ShoppingListId,
                                                         Title = jlr.ShoppingList.Title,
                                                         Approved = jlr.Approved,
                                                         Invited = jlr.Invited
                                                     })
                                                     .OrderByDescending(jlr => jlr.Invited)
                                                     .OrderBy(jlr => jlr.Title)
                                                     .ToListAsync();
        return requests;
    }

    public async Task<ReadJoinListRequestDto> AnswerRequest(string token, string userId, int shoppingListId, AnswerJoinListRequestDto dto)
    {
        var noBearer = token.Split(" ")[1];
        var session = await _context.UserTokens.FirstOrDefaultAsync(t => t.Value == noBearer);
        if (session == null) throw new UnauthorizedAccessException("Token inválido!!!");

        var requestAuthorId = session.UserId;
        var requestAuthor = await _context.Users.FirstOrDefaultAsync(u => u.Id == requestAuthorId);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null || requestAuthor is null) throw new Exception("Não foi encontrado um dos usuários envolvidos nessa requisição");

        var shoppingList = await _context.ShoppingList.Include(sl => sl.RelationMembersLists)
                                                      .Include(sl => sl.JoinListRequests)
                                                      .Select(sl => new
                                                      {
                                                        Id = sl.Id,
                                                        Title = sl.Title,
                                                        Admin = sl.RelationMembersLists.FirstOrDefault(rml =>
                                                                                              rml.IsAdmin == true
                                                                                              &&
                                                                                              rml.UserId == requestAuthorId
                                                                                              ),
                                                        Request = sl.JoinListRequests.FirstOrDefault(jlr => jlr.UserId == userId)
                                                        })
                                                      .FirstOrDefaultAsync(sl => sl.Id == shoppingListId)
                                                      ;
        if (shoppingList is null || shoppingList.Request is null) throw new Exception("Essa requisição não foi encontrada");

        if (shoppingList.Request.Invited is true && requestAuthor.Id != user.Id) throw new Exception("Esse usuário não pode responder esse convite");
        if (shoppingList.Request.Invited is false && requestAuthor.Id == user.Id) throw new Exception("Esse usuário não pode responder esse convite");
        
        void RootMethod()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                shoppingList.Request.Approved = dto.Approved;
                var createRelation = new CreateRelationMembersListsDto
                {
                    UserId = user.Id,
                    ShoppingListId = shoppingList.Id,
                    IsAdmin = false
                };
                var relation = _mapper.Map<RelationMembersLists>(createRelation);
                SomeMethod(relation);
                scope.Complete();
            }
            return;
        }
        void SomeMethod(RelationMembersLists relation)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if(dto.Approved is true)
                {
                    _context.RelationMembersLists.Add(relation);
                }
                _context.SaveChanges();
                scope.Complete();
            }
        }
        RootMethod();
        
        var readRequest = new ReadJoinListRequestDto
        {
            ShoppingListId = shoppingList.Id,
            ShoppingListName = shoppingList.Title,
            UserId = user.Id,
            UserName = user.Name,
            UserEmail = user.Email!,
            Invited = shoppingList.Request.Invited,
            Approved = shoppingList.Request.Approved
        };

        return readRequest;
    }
}
