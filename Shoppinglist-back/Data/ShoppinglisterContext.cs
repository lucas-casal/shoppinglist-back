using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Models;
namespace Shoppinglist_back.Data;

public class ShoppinglisterContext : DbContext
{
    public ShoppinglisterContext(DbContextOptions<ShoppinglisterContext> options) : base(options){}

    public DbSet<ShoppingList> ShoppingList {  get; set; }
    public DbSet<RelationMembersLists> RelationMembersLists { get; set; }
    public DbSet<RelationProductsLists> RelationProductsLists { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<RelatedUsers> RelatedUsers { get; set; }
    public DbSet<JoinListRequest> JoinListRequest { get; set; }
    public DbSet<RelatedUsersRequest> RelatedUsersRequest { get; set; }


   
}
