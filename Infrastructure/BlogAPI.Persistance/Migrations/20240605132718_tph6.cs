using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class tph6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseFiles_Articles_ArticleId",
                table: "BaseFiles");

            migrationBuilder.DropIndex(
                name: "IX_BaseFiles_ArticleId",
                table: "BaseFiles");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "BaseFiles");

            migrationBuilder.CreateTable(
                name: "ArticleArticleImageFile",
                columns: table => new
                {
                    ArticleImageFilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticlesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleArticleImageFile", x => new { x.ArticleImageFilesId, x.ArticlesId });
                    table.ForeignKey(
                        name: "FK_ArticleArticleImageFile_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleArticleImageFile_BaseFiles_ArticleImageFilesId",
                        column: x => x.ArticleImageFilesId,
                        principalTable: "BaseFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleArticleImageFile_ArticlesId",
                table: "ArticleArticleImageFile",
                column: "ArticlesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleArticleImageFile");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "BaseFiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseFiles_ArticleId",
                table: "BaseFiles",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseFiles_Articles_ArticleId",
                table: "BaseFiles",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
