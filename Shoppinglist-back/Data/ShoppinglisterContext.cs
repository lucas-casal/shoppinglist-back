using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Models;
namespace Shoppinglist_back.Data;

public class ShoppinglisterContext : DbContext
{
    public ShoppinglisterContext(DbContextOptions<ShoppinglisterContext> options) : base(options)
    {

    }

    public DbSet<ShoppingList> ShoppingList {  get; set; }
    public DbSet<RelationMembersLists> RelationMembersLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração do relacionamento entre User e RelationMembersLists
        modelBuilder.Entity<RelationMembersLists>()
            .HasOne(rml => rml.User)
            .WithMany(u => u.RelationMembersLists)
            .HasForeignKey(rml => rml.UserId);
    }

}
