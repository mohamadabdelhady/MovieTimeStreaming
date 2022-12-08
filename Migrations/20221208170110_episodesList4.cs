using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTimeStreaming.Migrations
{
    public partial class episodesList4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MediaEpisodes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "MediaEpisodes");
        }
    }
}
