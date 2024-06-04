using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class tph2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "BaseFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "BaseFiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "BaseFiles");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "BaseFiles");
        }
    }
}
