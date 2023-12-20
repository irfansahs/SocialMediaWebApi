using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media.Infrastructure.Migrations
{
    public partial class mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowerId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "FollowingId",
                table: "Follows");

            migrationBuilder.AddColumn<string>(
                name: "FollowTo",
                table: "Follows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Follows",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowTo",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Follows");

            migrationBuilder.AddColumn<int>(
                name: "FollowerId",
                table: "Follows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowingId",
                table: "Follows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
