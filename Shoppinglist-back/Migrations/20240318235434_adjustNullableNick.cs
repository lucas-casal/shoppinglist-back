using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoppinglist_back.Migrations
{
    /// <inheritdoc />
    public partial class adjustNullableNick : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NicknameB",
                table: "RelatedUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "NicknameA",
                table: "RelatedUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RelatedUsers",
                keyColumn: "NicknameB",
                keyValue: null,
                column: "NicknameB",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NicknameB",
                table: "RelatedUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "RelatedUsers",
                keyColumn: "NicknameA",
                keyValue: null,
                column: "NicknameA",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NicknameA",
                table: "RelatedUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
