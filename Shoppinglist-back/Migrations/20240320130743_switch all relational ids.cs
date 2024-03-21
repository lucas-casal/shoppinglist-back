using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoppinglist_back.Migrations
{
    /// <inheritdoc />
    public partial class switchallrelationalids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RelationProductsLists");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RelationMembersLists");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RelatedUsersRequest");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JoinListRequest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RelationProductsLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RelationMembersLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RelatedUsersRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JoinListRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
