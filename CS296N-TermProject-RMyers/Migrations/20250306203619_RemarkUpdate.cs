using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS296N_TermProject_RMyers.Migrations
{
    public partial class RemarkUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Starter_Articles_ArticleId",
                table: "Starter");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Starter",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Starter",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Response",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Starter_AuthorId",
                table: "Starter",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Response_AuthorId",
                table: "Response",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Response_AspNetUsers_AuthorId",
                table: "Response",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Starter_Articles_ArticleId",
                table: "Starter",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Starter_AspNetUsers_AuthorId",
                table: "Starter",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Response_AspNetUsers_AuthorId",
                table: "Response");

            migrationBuilder.DropForeignKey(
                name: "FK_Starter_Articles_ArticleId",
                table: "Starter");

            migrationBuilder.DropForeignKey(
                name: "FK_Starter_AspNetUsers_AuthorId",
                table: "Starter");

            migrationBuilder.DropIndex(
                name: "IX_Starter_AuthorId",
                table: "Starter");

            migrationBuilder.DropIndex(
                name: "IX_Response_AuthorId",
                table: "Response");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Starter");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Response");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Starter",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Starter_Articles_ArticleId",
                table: "Starter",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
