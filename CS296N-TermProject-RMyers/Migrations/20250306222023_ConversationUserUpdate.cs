using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS296N_TermProject_RMyers.Migrations
{
    public partial class ConversationUserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_AuthorId",
                table: "Conversations");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Conversations",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_AuthorId",
                table: "Conversations",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_AuthorId",
                table: "Conversations");

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "AuthorId",
                keyValue: null,
                column: "AuthorId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Conversations",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_AuthorId",
                table: "Conversations",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
