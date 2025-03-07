using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS296N_TermProject_RMyers.Migrations
{
    public partial class ConversationRestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Starter");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Conversations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Conversations",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Conversations",
                type: "varchar(2)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Conversations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Conversations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Conversations",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ArticleId",
                table: "Conversations",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_AuthorId",
                table: "Conversations",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_CategoryId",
                table: "Conversations",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Articles_ArticleId",
                table: "Conversations",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_AuthorId",
                table: "Conversations",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Categories_CategoryId",
                table: "Conversations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Articles_ArticleId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_AuthorId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Categories_CategoryId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_ArticleId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_AuthorId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_CategoryId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Conversations");

            migrationBuilder.CreateTable(
                name: "Starter",
                columns: table => new
                {
                    StarterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(type: "int", nullable: true),
                    AuthorId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<string>(type: "varchar(2)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starter", x => x.StarterId);
                    table.ForeignKey(
                        name: "FK_Starter_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId");
                    table.ForeignKey(
                        name: "FK_Starter_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Starter_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Starter_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Starter_ArticleId",
                table: "Starter",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Starter_AuthorId",
                table: "Starter",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Starter_CategoryId",
                table: "Starter",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Starter_ConversationId",
                table: "Starter",
                column: "ConversationId",
                unique: true);
        }
    }
}
