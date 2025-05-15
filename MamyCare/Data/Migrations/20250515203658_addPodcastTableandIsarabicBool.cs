using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamyCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class addPodcastTableandIsarabicBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArabic",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArabic",
                table: "Activities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Podcasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsArabic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podcasts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Podcasts");

            migrationBuilder.DropColumn(
                name: "IsArabic",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsArabic",
                table: "Activities");
        }
    }
}
