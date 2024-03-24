using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Data;
using Shoppinglist_back.Dtos.RelatedUsersDtos;
using Shoppinglist_back.Dtos.RelatedUsersRequestDtos;
using Shoppinglist_back.Models;
using System.Transactions;

namespace Shoppinglist_back.Services;

public class RelatedUsersRequestService
{
    private IMapper _mapper;
    private ShoppinglisterContext _context;
    public RelatedUsersRequestService(IMapper mapper, ShoppinglisterContext userContext)
    {
        _mapper = mapper;
        _context = userContext;
    }

    public async Task<RelatedUsersRequest> Create(CreateRelatedUsersRequestDto dto)
    {
        var relation = await _context.RelatedUsers.FirstOrDefaultAsync(rur =>
                                                (dto.UserAId == rur.UserAId || dto.UserAId == rur.UserBId)
                                                &&
                                                (dto.UserBId == rur.UserAId || dto.UserBId == rur.UserBId)
                                               );

        if (relation != null) throw new InvalidOperationException("Esses usuários já estão relacionados");

        var request = await _context.RelatedUsersRequest.FirstOrDefaultAsync(rur =>
                                                (dto.UserAId == rur.UserAId || dto.UserAId == rur.UserBId)
                                                &&
                                                (dto.UserBId == rur.UserAId || dto.UserBId == rur.UserBId)
                                               );

        var RURequest = new RelatedUsersRequest
        {
            UserAId = dto.UserAId,
            UserBId = dto.UserBId,
        };

        if (request is null)
        {
            await _context.RelatedUsersRequest.AddAsync(RURequest);
            await _context.SaveChangesAsync();
            return RURequest;
        }
        if (request!.Approved is not false) throw new OperationCanceledException($"Você já tem uma solicitação {(request.Approved is true ? "aprovada com" : "em análise por")} esse usuário");

        _context.RelatedUsersRequest.Remove(request);
        await _context.SaveChangesAsync();

        await _context.RelatedUsersRequest.AddAsync(RURequest);
        _context.SaveChanges();

        return RURequest;
    }

    public async Task<ReadRelatedUsersRequestDto> GetOne(string IdA, string IdB)
    {
        var request = await _context.RelatedUsersRequest
                                           .Include(rur => rur.UserA).Include(rur => rur.UserB)
                                           .FirstOrDefaultAsync(rur =>
                                               (IdA == rur.UserAId || IdA == rur.UserBId)
                                               &&
                                               (IdB == rur.UserAId || IdB == rur.UserBId)
                                            );
        if (request is null) throw new NullReferenceException("não foi encontrada nenhuma requisição entre esses usuários");
        
        var readRequest = new ReadRelatedUsersRequestDto
        {
            UserA = request.UserA.Name,
            UserB = request.UserB.Name,
            IsApproved = request.Approved
        };

        return readRequest;
    }

    public async Task<ICollection<ReadRelatedUsersRequestByUserIdDto>> GetAllByUserId(string id)
    {
        var relations = await _context.RelatedUsersRequest.Where(rur => rur.UserBId == id || rur.UserAId == id)
                                .Include(rur => rur.UserA).Include(rur => rur.UserB)
                                .Select(rur => new ReadRelatedUsersRequestByUserIdDto
                                {
                                    UserId = (id == rur.UserAId) ? rur.UserBId : rur.UserAId,
                                    UserName = (id == rur.UserAId) ? rur.UserB.Name : rur.UserA.Name,
                                    Approved = rur.Approved
                                }
                                ).ToListAsync();

        return relations;
    }

    public async Task Answer(RelatedUsersRequest dto)
    {
        var request = await _context.RelatedUsersRequest.FirstOrDefaultAsync(rur => (rur.UserAId == dto.UserAId) && (rur.UserBId == dto.UserBId));
        if (request is null) throw new NullReferenceException("não foi encontrada nenhuma requisição entre esses usuários");
        
        if (dto.Approved == false)
        {
            request.Approved = false;
            await _context.SaveChangesAsync();
            return;
        }

        void RootMethod()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                request!.Approved = true;
                var relation = _mapper.Map<RelatedUsers>(new CreateRelatedUsersDto { UserAId = request.UserAId, UserBId = request.UserBId });
                SomeMethod(relation);
                scope.Complete();
            }
            return;
        }
        void SomeMethod(RelatedUsers relation)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _context.RelatedUsers.Add(relation);
                _context.SaveChanges();
                scope.Complete();
            }
        }
        RootMethod();
    }

    public async Task DeleteOne(DeleteRelatedUsersRequestDto dto)
    {
        var request = await _context.RelatedUsersRequest.FirstOrDefaultAsync(rur =>
                                               (dto.UserAId == rur.UserAId || dto.UserAId == rur.UserBId)
                                               &&
                                               (dto.UserBId == rur.UserAId || dto.UserBId == rur.UserBId)
                                            );
        if (request == null) throw new NullReferenceException("Não foi encontrada uma requisição entre esses dois usuários");

        _context.RelatedUsersRequest.Remove(request);
        _context.SaveChanges();
        return;
    }

}


