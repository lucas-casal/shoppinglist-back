using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoppinglist_back.Migrations
{
    /// <inheritdoc />
    public partial class fixRelat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RelatedUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RelatedUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
