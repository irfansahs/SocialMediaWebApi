using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media.Infrastructure.Migrations
{
    public partial class Mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostImage",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emotion",
                table: "Comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Polarity",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostImage",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Emotion",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Polarity",
                table: "Comments");
        }
    }
}
