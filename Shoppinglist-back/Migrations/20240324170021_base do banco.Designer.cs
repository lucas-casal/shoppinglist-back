﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shoppinglist_back.Data;

#nullable disable

namespace Shoppinglist_back.Migrations
{
    [DbContext(typeof(ShoppinglisterContext))]
    [Migration("20240324170021_base do banco")]
    partial class basedobanco
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.JoinListRequest", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("int");

                    b.Property<bool?>("Approved")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Invited")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId", "ShoppingListId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("JoinListRequest");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelatedUsers", b =>
                {
                    b.Property<string>("UserAId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserBId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NicknameA")
                        .HasColumnType("longtext");

                    b.Property<string>("NicknameB")
                        .HasColumnType("longtext");

                    b.HasKey("UserAId", "UserBId");

                    b.HasIndex("UserBId");

                    b.ToTable("RelatedUsers");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelatedUsersRequest", b =>
                {
                    b.Property<string>("UserAId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserBId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("Approved")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserAId", "UserBId");

                    b.HasIndex("UserBId");

                    b.ToTable("RelatedUsersRequest");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelationMembersLists", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId", "ShoppingListId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("RelationMembersLists");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelationProductsLists", b =>
                {
                    b.Property<int>("ShoppingListId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("QuantityBought")
                        .HasColumnType("int");

                    b.Property<int>("QuantityWanted")
                        .HasColumnType("int");

                    b.HasKey("ShoppingListId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("RelationProductsLists");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.ShoppingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ShoppingList");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.JoinListRequest", b =>
                {
                    b.HasOne("Shoppinglist_back.Models.ShoppingList", "ShoppingList")
                        .WithMany("JoinListRequests")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoppinglist_back.Models.User", "User")
                        .WithMany("JoinListRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelatedUsers", b =>
                {
                    b.HasOne("Shoppinglist_back.Models.User", "UserA")
                        .WithMany("RelatedUsersAsUserA")
                        .HasForeignKey("UserAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoppinglist_back.Models.User", "UserB")
                        .WithMany("RelatedUsersAsUserB")
                        .HasForeignKey("UserBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserA");

                    b.Navigation("UserB");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelatedUsersRequest", b =>
                {
                    b.HasOne("Shoppinglist_back.Models.User", "UserA")
                        .WithMany("RelatedUsersRequestsAsUserA")
                        .HasForeignKey("UserAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoppinglist_back.Models.User", "UserB")
                        .WithMany("RelatedUsersRequestsAsUserB")
                        .HasForeignKey("UserBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserA");

                    b.Navigation("UserB");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelationMembersLists", b =>
                {
                    b.HasOne("Shoppinglist_back.Models.ShoppingList", "ShoppingList")
                        .WithMany("RelationMembersLists")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoppinglist_back.Models.User", "User")
                        .WithMany("RelationMembersLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.RelationProductsLists", b =>
                {
                    b.HasOne("Shoppinglist_back.Models.Product", "Product")
                        .WithMany("RelationProductsLists")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoppinglist_back.Models.ShoppingList", "ShoppingList")
                        .WithMany("RelationProductsLists")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.Product", b =>
                {
                    b.Navigation("RelationProductsLists");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.ShoppingList", b =>
                {
                    b.Navigation("JoinListRequests");

                    b.Navigation("RelationMembersLists");

                    b.Navigation("RelationProductsLists");
                });

            modelBuilder.Entity("Shoppinglist_back.Models.User", b =>
                {
                    b.Navigation("JoinListRequests");

                    b.Navigation("RelatedUsersAsUserA");

                    b.Navigation("RelatedUsersAsUserB");

                    b.Navigation("RelatedUsersRequestsAsUserA");

                    b.Navigation("RelatedUsersRequestsAsUserB");

                    b.Navigation("RelationMembersLists");
                });
#pragma warning restore 612, 618
        }
    }
}
