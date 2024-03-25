using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shoppinglist_back.Models;

namespace Shoppinglist_back.Data;

public class ShoppinglisterContext : IdentityDbContext<User>
{

    public ShoppinglisterContext(DbContextOptions<ShoppinglisterContext> opts) : base(opts){}
    public DbSet<ShoppingList> ShoppingList { get; set; }
    public DbSet<RelationMembersLists> RelationMembersLists { get; set; }
    public DbSet<RelationProductsLists> RelationProductsLists { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<RelatedUsers> RelatedUsers { get; set; }
    public DbSet<JoinListRequest> JoinListRequest { get; set; }
    public DbSet<RelatedUsersRequest> RelatedUsersRequest { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(l => l.UserId);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasKey(l => l.UserId);

        modelBuilder.Entity<IdentityUserToken<string>>()
            .HasKey(l => l.UserId);

        //Relationating Members and ShoppingLists
        modelBuilder.Entity<RelationMembersLists>()
        .HasKey(rml => new { rml.UserId, rml.ShoppingListId });

        modelBuilder.Entity<RelationMembersLists>()
            .HasOne(rml => rml.User)
            .WithMany(u => u.RelationMembersLists)
            .HasForeignKey(rml => rml.UserId);

        modelBuilder.Entity<RelationMembersLists>()
            .HasOne(rml => rml.ShoppingList)
            .WithMany(u => u.RelationMembersLists)
            .HasForeignKey(rml => rml.ShoppingListId);
        // END

        //Relationating Users
        modelBuilder.Entity<RelatedUsers>()
        .HasKey(ru => new { ru.UserAId, ru.UserBId });

        modelBuilder.Entity<RelatedUsers>()
            .HasOne(ru => ru.UserA)
            .WithMany(u => u.RelatedUsersAsUserA)
            .HasForeignKey(rml => rml.UserAId);

        modelBuilder.Entity<RelatedUsers>()
            .HasOne(ru => ru.UserB)
            .WithMany(u => u.RelatedUsersAsUserB)
            .HasForeignKey(ru => ru.UserBId);
        // END

        //Relationating Users and RelatedRequests
        modelBuilder.Entity<RelatedUsersRequest>()
        .HasKey(rur => new { rur.UserAId, rur.UserBId });

        modelBuilder.Entity<RelatedUsersRequest>()
            .HasOne(rur => rur.UserA)
            .WithMany(u => u.RelatedUsersRequestsAsUserA)
            .HasForeignKey(rur => rur.UserAId);

        modelBuilder.Entity<RelatedUsersRequest>()
            .HasOne(ru => ru.UserB)
            .WithMany(u => u.RelatedUsersRequestsAsUserB)
            .HasForeignKey(rur => rur.UserBId);
        // END

        //Relationating Users and JoinRequests
        modelBuilder.Entity<JoinListRequest>()
        .HasKey(jlr => new { jlr.UserId, jlr.ShoppingListId });

        modelBuilder.Entity<JoinListRequest>()
            .HasOne(jlr => jlr.User)
            .WithMany(u => u.JoinListRequests)
            .HasForeignKey(jlr => jlr.UserId);

        modelBuilder.Entity<JoinListRequest>()
            .HasOne(jlr => jlr.ShoppingList)
            .WithMany(u => u.JoinListRequests)
            .HasForeignKey(jlr => jlr.ShoppingListId);
        // END

        //Relationating Users and JoinRequests
        modelBuilder.Entity<RelationProductsLists>()
        .HasKey(rpl => new { rpl.ShoppingListId, rpl.ProductId });

        modelBuilder.Entity<RelationProductsLists>()
            .HasOne(rpl => rpl.ShoppingList)
            .WithMany(sl => sl.RelationProductsLists)
            .HasForeignKey(rpl => rpl.ShoppingListId);

        modelBuilder.Entity<RelationProductsLists>()
            .HasOne(rpl => rpl.Product)
            .WithMany(p => p.RelationProductsLists)
            .HasForeignKey(jlr => jlr.ProductId);
        // END
    }
}
