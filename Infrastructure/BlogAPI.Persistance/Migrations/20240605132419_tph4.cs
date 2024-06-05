using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class tph4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
