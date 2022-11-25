using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTimeStreaming.Migrations
{
    public partial class edit20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaDuration",
                table: "Reviews");

            migrationBuilder.AddColumn<double>(
                name: "MediaDuration",
                table: "Media",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaDuration",
                table: "Media");

            migrationBuilder.AddColumn<double>(
                name: "MediaDuration",
                table: "Reviews",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
