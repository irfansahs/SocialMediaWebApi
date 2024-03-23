using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media.Infrastructure.Migrations
{
    public partial class TranslateServiceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceLanguageCode",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranslatedPost",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceLanguageCode",
                table: "Comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranslatedPost",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceLanguageCode",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TranslatedPost",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SourceLanguageCode",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TranslatedPost",
                table: "Comments");
        }
    }
}
