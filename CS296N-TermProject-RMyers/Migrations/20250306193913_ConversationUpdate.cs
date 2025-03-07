using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS296N_TermProject_RMyers.Migrations
{
    public partial class ConversationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Entries_ArticleId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_AspNetUsers_AuthorId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Categories_CategoryId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Starter_Categories_CategoryId",
                table: "Starter");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_ArticleId",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entries",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_CategoryId",
                table: "Articles",
                newName: "IX_Articles_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_AuthorId",
                table: "Articles",
                newName: "IX_Articles_AuthorId");

            migrationBuilder.UpdateData(
                table: "Starter",
                keyColumn: "CategoryId",
                keyValue: null,
                column: "CategoryId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Starter",
                type: "varchar(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Starter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Starter_ArticleId",
                table: "Starter",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Starter_Articles_ArticleId",
                table: "Starter",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Starter_Categories_CategoryId",
                table: "Starter",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Starter_Articles_ArticleId",
                table: "Starter");

            migrationBuilder.DropForeignKey(
                name: "FK_Starter_Categories_CategoryId",
                table: "Starter");

            migrationBuilder.DropIndex(
                name: "IX_Starter_ArticleId",
                table: "Starter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Starter");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Entries");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryId",
                table: "Entries",
                newName: "IX_Entries_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AuthorId",
                table: "Entries",
                newName: "IX_Entries_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Starter",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Conversations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entries",
                table: "Entries",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ArticleId",
                table: "Conversations",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Entries_ArticleId",
                table: "Conversations",
                column: "ArticleId",
                principalTable: "Entries",
                principalColumn: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_AspNetUsers_AuthorId",
                table: "Entries",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Categories_CategoryId",
                table: "Entries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Starter_Categories_CategoryId",
                table: "Starter",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
